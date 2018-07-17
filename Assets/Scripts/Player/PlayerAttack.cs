using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death
}

public enum AttackState
{
    Moving,
    Idle,
    Attack
}

public class PlayerAttack : MonoBehaviour {

    public PlayerState state = PlayerState.ControlWalk;
    public AttackState attackState = AttackState.Idle;

    public string aniname_normalattack;
    public string aniname_idle;
    public string aniname_now;
    public float time_normalattack;
    public float rate_normalattack = 1;
    public float minDistance = 5;
    public float normalAttackDamage = 10;
    public GameObject effect;
    public float missRate = 0.3f;
    public GameObject hudtextPrefab;
    public AudioClip missSound;

    private Transform targetTransform;
    private float timer = 0;
    private PlayerMove playerMove;
    private new Animation animation;
    private bool showEffect = false;
    private PlayerStatus ps;
    private GameObject hudtextGo;
    private GameObject hudTextFollow;
    private HUDText hudtext;

    public GameObject[] efxs;
    private Dictionary<string, GameObject> efxMap = new Dictionary<string, GameObject>();

    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        animation = GetComponent<Animation>();
        ps = GetComponent<PlayerStatus>();
        hudTextFollow = transform.Find("HUDText").gameObject;

        foreach(var item in efxs)
        {
            efxMap.Add(item.name, item);
        }
    }

    void Start()
    {
        hudtextGo = NGUITools.AddChild(HudTextParent._Instance.gameObject, hudtextPrefab);
        hudtext = hudtextGo.GetComponent<HUDText>();
        UIFollowTarget follow = hudtextGo.GetComponent<UIFollowTarget>();
        follow.target = hudTextFollow.transform;
        follow.gameCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && state != PlayerState.Death)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.tag.Equals(Tags.enemy))
            {
                state = PlayerState.NormalAttack;
                targetTransform = raycastHit.collider.gameObject.transform;
                timer = 0;
                showEffect = false;
            }
            else
            {
                targetTransform = null;
                state = PlayerState.ControlWalk;
            }
        }

        if(state == PlayerState.NormalAttack)
        {
            if(targetTransform == null)
            {
                state = PlayerState.ControlWalk;
                attackState = AttackState.Idle;
                return;
            }

            float distance = Vector3.Distance(transform.position, targetTransform.position);
            if(distance <= minDistance)
            {//进行攻击
                transform.LookAt(targetTransform);
                attackState = AttackState.Attack;
                timer += Time.deltaTime;
                animation.CrossFade(aniname_now);
                if(timer >= time_normalattack)
                {
                    aniname_now = aniname_idle;
                    if(!showEffect)
                    {
                        showEffect = true;
                        //播放特效
                        GameObject.Instantiate(effect, targetTransform.position, Quaternion.identity);
                        targetTransform.GetComponent<WolfBaby>().TakeDamage(GetAttack());
                    }
                }
                if(timer >= (1f / rate_normalattack))
                {
                    timer = 0;
                    showEffect = false;
                    aniname_now = aniname_normalattack;
                }
            }
            else
            {//走向敌人
                attackState = AttackState.Moving;
                playerMove.SimpleMove(targetTransform.position);
            }
        }
    }

    public int GetAttack()
    {
        return (int)(Equipment._Instance.attack + ps.attack + ps.attack_plus);
    }
    
    public void TakeDamage(int attack)
    {
        if(state == PlayerState.Death)
        {
            return;
        }

        float def = Equipment._Instance.defend + ps.def + ps.def_plus;
        float temp = attack * ((200 - def) / 200);
        if(temp < 1)
        {
            temp = 1;
        }

        float value = Random.Range(0f, 1f);
        if(value < missRate)
        {
            AudioSource.PlayClipAtPoint(missSound, transform.position);
            hudtext.Add("MISS", Color.gray, 1);
        }
        else
        {
            Debug.Log("been attach");
            hudtext.Add("-" + temp, Color.red, 1);
            ps.hpRemain -= temp;
            if(ps.hpRemain <= 0)
            {
                ps.hpRemain = 0;
                state = PlayerState.Death;
            }
        }
    }

    void OnDestroy()
    {
        GameObject.Destroy(hudtextGo);
    }

    public void UseSkill(SkillInfo info)
    {
        if(ps.heroType == HeroType.Magician && info.applicableRole == ApplicableRole.Swordman)
        {
            return;
        }
        if(ps.heroType == HeroType.Swordman && info.applicableRole == ApplicableRole.Magician)
        {
            return;
        }

        switch(info.applyType)
        {
            case ApplyType.Passive:
                StartCoroutine(OnPassiveSkillUse(info));
                break;
            case ApplyType.Buff:
                StartCoroutine(OnBuffSkillUse(info));
                break;
        }
    }

    IEnumerator OnPassiveSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        animation.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerState.ControlWalk;
        int hp = 0, mp = 0;
        if(info.applyProperty == ApplyProperty.HP)
        {
            hp = info.applyValue;
        }
        else if(info.applyProperty == ApplyProperty.MP)
        {
            mp = info.applyValue;
        }

        ps.PlusHpAndMp(hp, mp);

        GameObject effectPrefab = null;
        if(efxMap.TryGetValue(info.efxName, out effectPrefab))
        {
            GameObject.Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator OnBuffSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        animation.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerState.ControlWalk;
        
        switch(info.applyProperty)
        {
            case ApplyProperty.Attack:
                ps.attack_plus = GetAttack() * (info.applyValue / 100f);
                break;
            case ApplyProperty.Def:
                ps.def_plus *= (info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                ps.speed_plus *= (info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack *= (info.applyValue / 100);
                break;
        }

        GameObject effectPrefab = null;
        if(efxMap.TryGetValue(info.efxName, out effectPrefab))
        {
            GameObject.Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }
    }
}
