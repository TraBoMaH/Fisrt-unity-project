/*using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firePoint;
    public float range = 50f;
    public float fireRate = 1f;
    public int damage = 10;

    private float nextTimeToFire = 1f;  

    void Update()
    {
        // Check if there's a target within range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                // Calculate direction to the enemy
                Vector3 directionToEnemy = col.transform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, directionToEnemy);

                // Check if the enemy is within turret's field of view
                if (angle < 45f)
                {
                    // Check if enough time has passed to fire another shot
                    if (Time.time >= nextTimeToFire)
                    {
                        // Cast a ray from firePoint to the enemy
                        RaycastHit hit;
                        if (Physics.Raycast(firePoint.position, directionToEnemy, out hit, range))
                        {
                            if (hit.collider.CompareTag("Enemy"))
                            {
                                // Deal damage to the enemy
                                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                                if (enemyHealth != null)
                                {
                                    enemyHealth.TakeDamage(damage);
                                }
                            }
                        }

                        // Set next time to fire
                        nextTimeToFire = Time.time + 1f / fireRate;
                    }
                }
            }
        }
    }
}*/
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public Transform firePoint;
    private float range = 100f;
    private float fireRate = 2f;
    private int damage = 10;
    private float nextTimeToFire = 2f; 
    private Transform target; // Ссылка на текущую цель
    public Transform myself; //эксперемнталная

    void Update()
    {
        // Поиск врагов в заданной области
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                // Вычисление направления к врагу
                Vector3 directionToEnemy = col.transform.position - transform.position;
                    // Проверка, прошло ли достаточно времени для следующего выстрела
                    if (Time.time >= nextTimeToFire)
                    {
                        // Выстрел врага
                        RaycastHit hit; Debug.Log("труель стреляет");
                        if (Physics.Raycast(firePoint.position, directionToEnemy, out hit, range))
                        {
                            if (hit.collider.CompareTag("Enemy"))
                            {
                                // Нанесение урона врагу
                                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                                if (enemyHealth != null)
                                {
                                    enemyHealth.TakeDamage(damage); Debug.Log("Турель попала");
                                }
                            }
                        }

                        // Установка времени следующего выстрела
                        nextTimeToFire = Time.time + 1f / fireRate;
                    }
                //}

                // Установка текущей цели
                target = col.transform;
            }
        }

        // Если есть цель, вращаем объект к ней
        if (target != null)
        {
           myself.LookAt(target);

        }
    }
}
// чтобв турель попадала надо точку стрельбы поднять выше дула