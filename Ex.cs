using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex : MonoBehaviour
{
private EnemyHealth enemyHealth;
private void Start() {
    enemyHealth = GetComponent<EnemyHealth>();
}
public void A()
{
    if (enemyHealth.currentHealth <= 0)
    {
        RaidBasics.Pointsplus();
    }
}
private void OnDestroy() 
{
    A();
}
}
