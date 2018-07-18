using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour 
{
    public float attack = 0;

    private List<WolfBaby> wolfList = new List<WolfBaby>();

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.enemy)
        {
            GameObject go = other.gameObject;
            WolfBaby wolf = go.GetComponent<WolfBaby>();
            int index = wolfList.IndexOf(wolf);
            if (index == -1)
            {
                wolf.TakeDamage((int)attack);
                wolfList.Add(wolf);
            }
        }
    }
}
