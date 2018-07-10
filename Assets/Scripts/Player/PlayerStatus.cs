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

    public int level = 1;
    public int hp = 100;
    public int mp = 100;
    public float hpRemain = 100;
    public float mpRemain = 100;
    public int coin = 200;

    public int attack = 20;
    public int attack_plus = 0;
    public int def = 20;
    public int def_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;

    public int poin_remain = 0;

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
}
