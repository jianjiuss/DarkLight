using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBabySpawn : MonoBehaviour 
{

    public int maxCount = 5;
    public int refreshTime = 2;
    public GameObject wolfBabyPrefabs;

    private int currentCount = 0;
    private float timer = 0;
    
	void Start () {
		
	}
	
	void Update () {
		if(currentCount >= maxCount)
        {
            return;
        }

        if(timer >= refreshTime)
        {
            timer = 0;
            Vector3 pos = transform.position;
            pos.x += Random.Range(-5,5);
            pos.z += Random.Range(-5,5);
            GameObject go = GameObject.Instantiate(wolfBabyPrefabs, pos, Quaternion.identity);
            go.GetComponent<WolfBaby>().SetSpawn(this);
            currentCount++;
        }
        timer += Time.deltaTime;
	}

    public void SubNum(int value = 1)
    {
        currentCount -= value;
    }
}
