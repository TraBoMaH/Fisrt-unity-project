using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaidBasics : MonoBehaviour
{
    public GameObject player;
    public Transform targetPosition; // Целевая позиция для перемещения объекта
    public GameObject objectToActivate; // Объект, который нужно активировать
    public GameObject replacementObject; // Объект, который появится на месте уничтоженного объекта
    private static int PointsToLeave = 0; // Счетчик уничтоженных объектов
    private GameObject builded;
    public GameObject DestinationPoint;
public void TpIn()
{
    player.transform.position = targetPosition.position;
    objectToActivate.SetActive(true);
}
private void TpOut()
{
    player.transform.position = DestinationPoint.transform.position;
    builded = Instantiate(replacementObject, objectToActivate.transform.position, Quaternion.identity);
    Destroy(objectToActivate); objectToActivate = builded; AirStrike.PlusCount(); health.GetMoreFlasks(1);
    PointsToLeave = 0;
}
public static void Pointsplus()
{
    PointsToLeave += 1;
}
private void Update() {
    if(Input.GetKeyDown(KeyCode.End)){ TpIn(); } // СНЕСТИ ПОТОМ
    if(Input.GetKeyDown(KeyCode.PageDown)){Pointsplus(); Debug.Log("есть");}
        if (PointsToLeave >= 5)
    {
        TpOut();
    }
}
}