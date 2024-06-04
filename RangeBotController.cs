using System.Collections;
using UnityEngine; 
using UnityEngine.AI;

public class RangeBotController : MonoBehaviour
{
    private Transform target; // Целевая точка, к которой двигается бот
    public float attackRange = 2f; // Дистанция для атаки
    public float attackCooldown = 1f; // Время между атаками
    private float lastAttackTime;
    public LayerMask mask;
    public NavMeshAgent agent;
    public Transform gizmocenter;
    private Animator animator;
    public GameObject attackObject;
    public byte attackObjectSpeed = 10;
    public GameObject spawnplace;
    public AudioClip expAudio; // звук взрыва плевка
    public AudioSource source;
    public GameObject Particle;

    private void Start()
    {
    target = GameObject.Find("Player 2.0").transform;
    animator = GetComponent<Animator>();
    }

    private void Update()
    {
       
       agent.destination = target.position;


        // Проверяем, находится ли цель в дистанции атаки
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            // Проверяем, прошло ли достаточно времени с последней атаки
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                StartCoroutine(Attack());
                lastAttackTime = Time.time;
            }
        }
    }

    private IEnumerator Attack()
    {
           // Создаем объект из префаба
        GameObject enemy = Instantiate(attackObject, spawnplace.transform.position, Quaternion.identity);

        // Получаем ссылку на Rigidbody объекта
        Rigidbody rb = enemy.GetComponent<Rigidbody>();

        // Задаем скорость движения объекта
        rb.velocity = transform.forward * attackObjectSpeed;
        
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2);
            
            Collider[] hitEnemies = Physics.OverlapSphere(enemy.transform.position, 100, 1 << 8);// если враг с тэгом не умирает, скорее всего у него стоит другой layer. layer != tag

        // Применение урона к каждому врагу
        foreach (Collider target in hitEnemies)
        {
            health hp = target.GetComponent<health>();
            hp.takedmg(20f); 
        }
        
        Instantiate(Particle, enemy.transform.position, Quaternion.identity); source.PlayOneShot(expAudio); Destroy(enemy); 
    }

    // Рисуем гизмо для визуализации дальности атаки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmocenter.position, 1.5f);
    }
}
