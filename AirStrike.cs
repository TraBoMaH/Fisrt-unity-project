/*
using UnityEngine;

public class AirStrike : MonoBehaviour
{
    public GameObject planePrefab; // Префаб самолета
    public GameObject bombPrefab; // Префаб бомбы
    public float planeSpeed = 10f; // Скорость самолета
    public float bombSpeed = 20f; // Скорость бомбы
    public float distanceToTarget = 50f; // Расстояние до цели

    private bool isAirstrikeInProgress = false;
    private GameObject spawnedPlane; // Глобальная переменная для самолета

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAirstrikeInProgress)
        {
            // Вызываем авиаудар
            GetTargetPosition();StartAirStrike();
        }
    }

    private void StartAirStrike()
    {
        isAirstrikeInProgress = true;

        // Спавним самолет
        Vector3 spawnPosition = transform.position + Vector3.up * 10f + Vector3.back * 30f;
        spawnedPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);

        // Запускаем самолет вперед
        Rigidbody planeRb = spawnedPlane.GetComponent<Rigidbody>();
        planeRb.velocity = transform.forward * planeSpeed;

        // Запускаем таймер для бомбы
        Invoke("DropBomb", 5f);
    }

    private void DropBomb()
    {
        // Спавним бомбу в текущем положении самолета
        GameObject bomb = Instantiate(bombPrefab, spawnedPlane.transform.position, Quaternion.identity);

        // Запускаем бомбу к цели
        Vector3 targetPosition = GetTargetPosition();
        Vector3 bombDirection = (targetPosition - bomb.transform.position).normalized;
        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();
        bombRb.velocity = bombDirection * bombSpeed;

        // Уничтожаем самолет
        Destroy(spawnedPlane, 3f);
    }

    private Vector3 GetTargetPosition()
    {
        // Вычисляем цель с помощью Raycast от центра камеры
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            return hit.point;
        }

        // Если Raycast не попал в коллайдер, возвращаем точку впереди
        return transform.position + transform.forward * distanceToTarget;
    }
}*/
using System.Runtime.CompilerServices;
using UnityEngine;

public class AirStrike : MonoBehaviour
{
    public GameObject planePrefab; // Префаб самолета
    public GameObject bombPrefab; // Префаб бомбы
    public float planeSpeed = 10f; // Скорость самолета
    public float bombSpeed = 20f; // Скорость бомбы
    private bool isAirstrikeInProgress = false;
    private GameObject spawnedPlane; // Глобальная переменная для самолета
    private Vector3 Target;
    private GameObject bomb;
    public GameObject explosion;
    public AudioClip expAudio; public AudioSource source;
    public static int AirStrikecount; // сейвить
    /*private void Start() {
        source = GetComponentInChildren<AudioSource>();
    }*/
    private void Update()
    {
        if(Target != null)
        {
            if (Input.GetKeyDown(KeyCode.F1) && !isAirstrikeInProgress)
            {
                CallStrike();
            }
        }
        
    }

    private void StartAirStrike()
    {
        isAirstrikeInProgress = true;

        // Спавним самолет сверху сзади игрока
        Vector3 spawnPosition = Target + Vector3.up * 10f - transform.forward * 70f;
        spawnedPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);

        // Запускаем самолет вперед
        Rigidbody planeRb = spawnedPlane.GetComponent<Rigidbody>();
        planeRb.velocity = transform.forward * planeSpeed;

        // Вращаем самолет в направлении движения
        Vector3 planeDirection = planeRb.velocity.normalized;
        spawnedPlane.transform.LookAt(spawnedPlane.transform.position + planeDirection);

        // Запускаем таймер для бомбы
        Invoke("DropBomb", 5f); Invoke("babax" , 5.7f);
    }

    private void DropBomb()
    {
        // Спавним бомбу в текущем положении самолета
        bomb = Instantiate(bombPrefab, spawnedPlane.transform.position, Quaternion.identity);

        // Запускаем бомбу к цели
        Vector3 targetPosition = Target;
        Vector3 bombDirection = (targetPosition - bomb.transform.position).normalized;
        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();
        bombRb.velocity = bombDirection * bombSpeed;

        // Уничтожаем самолет
        Destroy(spawnedPlane, 5f); Destroy(bomb, 0.8f);
        
    }

    private void GetTargetPosition()
    {
  // Создаем луч, начиная от текущей позиции объекта в направлении вниз (по оси -Y)
    Camera playerCamera = Camera.main;
    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Пускаем луч и получаем информацию о столкновении
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Если луч пересекся с каким-то объектом, записываем его трансформ
            Target = hit.point;
        }
}
private void babax()
{
    Collider[] hitEnemies = Physics.OverlapSphere(bomb.transform.position, 100, 1 << 8);// если враг с тэгом не умирает, скорее всего у него стоит другой layer. layer != tag

        // Применение урона к каждому врагу
        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth hp = enemy.GetComponent<EnemyHealth>();
            hp.TakeDamage(200f); 
        }
   GameObject exp = Instantiate(explosion, bomb.transform.position, Quaternion.identity);
   ParticleSystem exps = exp.GetComponentInChildren<ParticleSystem>(); exps.Play(); source.PlayOneShot(expAudio);
   Destroy(exp, 5); isAirstrikeInProgress = false;
}
public void CallStrike()
{
    if(AirStrikecount >= 0)
    {
        GetTargetPosition(); StartAirStrike(); AirStrikecount -= 1;
    }
}
public static void PlusCount()
{
    AirStrikecount += 1;
}
} // осталось найти звуки джетов и сделать отдельный скрипт под звук рафаля