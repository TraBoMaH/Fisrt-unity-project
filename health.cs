using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour 
{
public float Maxhp = 100;
public static float righthp;
public Image hpbar;
public static int flasks; // сейвить
    void Start()
    {
        righthp = Maxhp;
    }
   public void takedmg(float dmg)
    {
        righthp -= dmg;
        hpbar.fillAmount = righthp / 100f;
        if(righthp <= 20 && flasks > 0)
        {
            righthp += 50; flasks--;
        }
        if(righthp <= 0){
            Destroy(gameObject);
        } 
    }
    public static void GetMoreFlasks(int a)
    {
        flasks += a;
    }
}
   