using UnityEngine; using UnityEngine.AI;

public class BotContrtrain : MonoBehaviour
{
    private Transform target; // Целевая точка, к которой двигается бот
    public float attackRange = 2f; // Дистанция для атаки
    public float attackCooldown = 1f; // Время между атаками
    private float lastAttackTime;
    public LayerMask mask;
    public NavMeshAgent agent;
    public Transform gizmocenter;
    private Animator animator;
    public float damage;

    private void Start()
    {
    target = GameObject.Find("PlayerTrain").transform;
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
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        // Обнаружение врагов в пределах дальности атаки
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, mask = 1 << 3);
        animator.SetTrigger("Attack");

        // Применение урона к каждому врагу
        foreach (Collider enemy in hitEnemies)
        {
            // Предполагаем, что у врага есть компонент Health
            health playerhp = enemy.GetComponent<health>();
            if (playerhp != null)
            {
                playerhp.takedmg(damage); // Установите значение урона по необходимости
            }
        }
    }

    // Рисуем гизмо для визуализации дальности атаки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmocenter.position, 1.5f);
    }





}


