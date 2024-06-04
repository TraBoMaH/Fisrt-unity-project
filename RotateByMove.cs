using System.Collections;
using UnityEngine;

public class RotateByMove : MonoBehaviour
{
    private void Update()
    {
        // Получаем направление движения объекта (в данном случае, вперед)
        Vector3 moveDirection = transform.forward;

        // Вычисляем угол между текущим направлением объекта и его движением
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        // Плавно поворачиваем объект в направлении движения
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 100f * Time.deltaTime);
    }
}
