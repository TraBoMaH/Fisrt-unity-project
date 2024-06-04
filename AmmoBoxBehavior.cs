using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Shooting.PickUpBullets(150);
        ShotgunShoot.PickUpBullets(30);
        UziShooting.PickUpBullets(300);
    }
}