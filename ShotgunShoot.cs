using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Eventing.Reader;

public class ShotgunShoot : MonoBehaviour
{
    public GameObject hitPrefab; // Префаб объекта, который будет спавниться при попадании
    public Transform shootPoint; // Точка, откуда будет производиться выстрел
    public int pelletsCount = 10; // Количество снарядов (дробинок) при каждом выстреле
    public float spreadAngle = 5f; // Угол рассеивания дроби
    private float nextfire = 0;
    public float firerate;
    public int MaxMag = 6;
    public int MaxReserveMag;
    private static int MaxReserveMag2;
    private int NowMaxMag;
    private static int NowReserveMag;
    public AudioClip reload;
    private AudioSource AudioSource;
    private GameObject ammobox;
    public Recoil recoil;
    public AudioClip audioclip;
    private ParticleSystem particle;
    private Animator animator;
    public TextMeshProUGUI NowAmmoMag;
    public TextMeshProUGUI NowReserveAmmo;
    private bool ReloadState = false;
    private void Start() 
    {
        NowMaxMag = MaxMag; NowReserveMag = MaxReserveMag; MaxReserveMag2 = MaxReserveMag;
        AudioSource = GetComponent<AudioSource>();
        ammobox = GameObject.Find("ammobox");
        particle = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ShowAmmo();
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextfire)
        {
            if(ReloadState == false)
            {
            nextfire = Time.time + 1f / firerate;
            Shoot();
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(ReloadState == false)
            {
            StartCoroutine(Reload());
            }
        }
    }

    private void Shoot()
    {
        if(NowMaxMag > 0)
        {   
            recoil.Fire();
            AudioSource.PlayOneShot(audioclip);
            particle.Play();NowMaxMag--;

            for (int i = 0; i < pelletsCount; i++)
            {
                Vector3 direction = shootPoint.forward / -1;
                direction.x += Random.Range(-spreadAngle, spreadAngle);
                direction.y += Random.Range(-spreadAngle, spreadAngle);

                if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit))
                {
                // Создание объекта в точке попадания
                GameObject impact = Instantiate(hitPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 1f);
                    if(hit.collider.CompareTag("Enemy"))
                    {
                        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(30);
                        }
                    }
                }
            }
        }
        
    }
    private IEnumerator Reload()
    {
        ReloadState = true;
        animator.SetTrigger("Reload");
        AudioSource.PlayOneShot(reload);
        yield return new WaitForSeconds(2);
        int reason = MaxMag - NowMaxMag;
        int bulletsToReload = Mathf.Min(reason, NowReserveMag);
        NowReserveMag -= bulletsToReload;
        NowMaxMag += bulletsToReload;
        ReloadState = false;
    }
    public static void PickUpBullets(int bullets)
    {
        NowReserveMag = Mathf.Min(NowReserveMag + bullets, MaxReserveMag2);
    }
        private void ShowAmmo()
    {
        NowAmmoMag.text = NowMaxMag.ToString();
        NowReserveAmmo.text = NowReserveMag.ToString();
    }
}