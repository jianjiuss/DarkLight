using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    Swordman,
    Magician,
    Common
}

public class PlayerStatus : MonoBehaviour 
{
    public HeroType heroType;

    public int level = 1; //UpGradeExp = 100 + level * 30
    public int hp = 100;
    public int mp = 100;
    public float hpRemain = 100;
    public float mpRemain = 100;
    public int coin = 200;

    public int attack = 20;
    public float attack_plus = 0;
    public int def = 20;
    public float def_plus = 0;
    public int speed = 20;
    public float speed_plus = 0;
    public float exp = 0;


    public int poin_remain = 0;

    private void Start()
    {
        GetExp(0);
    }

    public void GetCoin(int count)
    {
        coin += count;
        Inventory._Instance.UpdateCoinLabel();
    }

    public bool GetPoint(int point = 1)
    { 
        if(poin_remain >= point)
        {
            poin_remain -= point;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetMoney(int spendMoney)
    {
        if(spendMoney > coin)
        {
            return false;
        }

        coin -= spendMoney;
        Inventory._Instance.UpdateCoinLabel();
        return true;
    }

    public void GetExp(int value)
    {
        exp += value;
        float totalExp = 100 + level * 30;
        while(exp >= totalExp)
        {
            level++;
            exp -= totalExp;
            totalExp = 100 + level * 30;
        }
        ExpBar._Instance.SetValue(exp / totalExp);
    }

    public void PlusHpAndMp(int plusHp, int plusMp)
    {
        hpRemain += plusHp;
        mpRemain += plusMp;

        if(hpRemain > hp)
        {
            hpRemain = hp;
        }
        if(mpRemain > mp)
        {
            mpRemain = mp;
        }
    }

    public bool TakeMP(int value)
    {
        if(value > mpRemain)
        {
            return false;
        }

        mpRemain -= value;
        return true;
    }
}
