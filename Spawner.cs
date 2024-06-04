using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine; using TMPro;

public class Spawner : MonoBehaviour
{
public GameObject to1spawn;
public GameObject to2spawn;
public GameObject to3spawn;
public Transform spawnpos;
private int count = 0;
public static int wave; // сейвить , static?
private int points;
public TextMeshProUGUI waves; // для отображения волн
public static int ZombieAmount = 10; // сейвить , static?
public static float interval = 3; // сейвить , static?

public IEnumerator Spawn()
{

    if(count <= ZombieAmount && count >= 1 && wave <= 10)
    {
        Instantiate(to1spawn, spawnpos.position, Quaternion.identity);
    count++;
    yield return new WaitForSeconds(interval); StartCoroutine(Spawn());
        if(count >= ZombieAmount)
        {
            
            wave++;
            points = 10 * wave / 2;
            Money.Addmoney(points);
            waves.text = wave.ToString();
            ZombieAmount++;
            count = 0;
            if(interval > 1)
            {
                interval -= 0.1f;
            }
        }
    }
    else if (count <= ZombieAmount && count >= 1 && wave >= 10)
    {
        Instantiate(to1spawn, spawnpos.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        if(count % 2 == 0)
        {
            Instantiate(to2spawn, spawnpos.position, Quaternion.identity);
        }
        count++; yield return new WaitForSeconds(interval); StartCoroutine(Spawn());
        if(count >= ZombieAmount)
        {
            wave++;
            points = 10 * wave / 2;
            Money.Addmoney(points);
            waves.text = wave.ToString();
            ZombieAmount++;
            count = 0;
            if(interval > 1)
            {
                interval -= 0.1f;
            }
        }
    }
    else if(count <= ZombieAmount && count >= 1 && wave % 10 == 0)
    {
                Instantiate(to1spawn, spawnpos.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        if(count % 2 == 0)
        {
            Instantiate(to2spawn, spawnpos.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        if(count == ZombieAmount / 2)
        {
            Instantiate(to3spawn, spawnpos.position, Quaternion.identity);
        }
        count++; yield return new WaitForSeconds(interval); StartCoroutine(Spawn());
        if(count >= ZombieAmount)
        {
            wave++;
            points = 10 * wave / 2;
            Money.Addmoney(points);
            waves.text = wave.ToString();
            ZombieAmount++;
            count = 0;
            if(interval > 1)
            {
                interval -= 0.1f;
            }
        }
    }   
}
public void StartWave()
{
    count = 1;
    StartCoroutine(Spawn());
}
}
