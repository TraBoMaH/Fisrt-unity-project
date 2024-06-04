using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UziShooting : MonoBehaviour // Сделано потому что почему то пулемет и узи на 1 скрипте
{
    public AudioSource audiosource;
    public AudioClip audioclip;
    public float firerate = 30f;
    public ParticleSystem muzzle;
    private float nextfire = 0f;
    public GameObject hiteffect;
    public int MaxMag = 10;
    public int MaxReserveMag;
    private static int MaxReserveMag2;
    private int NowMaxMag;
    private static int NowReserveMag;
    public Camera playerCamera; // Ссылка на камеру
    public float hitRange = 100f; // Дальность хитскана
    public TextMeshProUGUI NowAmmoMag;
    public TextMeshProUGUI NowReserveAmmo;
    public AudioClip reload;
    private GameObject AmmoBox; // временно
    public Recoil recoilObj;
    private Animator animator2;
    private bool reloadState = false;
    private void Start()
    {
        NowReserveMag = MaxReserveMag; NowMaxMag = MaxMag; MaxReserveMag2 = MaxReserveMag;
        recoilObj = GameObject.Find("Camera").GetComponent<Recoil>();
        AmmoBox = GameObject.Find("ammobox");
        animator2 = GetComponent<Animator>();
        
    }
    private void Update()
    {
        ShowAmmo();    
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(reloadState == false)
            {
                StartCoroutine(Reload());
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextfire)
        {
        if(reloadState == false)
        {
        nextfire = Time.time + 1f / firerate;
        Shoot();
        }
        
        }    
        
        
    }

    private void Shoot()
    {
        if(NowMaxMag > 0)
        {
        audiosource.PlayOneShot(audioclip);
        muzzle.Play();NowMaxMag--;recoilObj.Fire(); 

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, hitRange))
        {
            GameObject impact = Instantiate(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);

            
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                 if (enemyHealth != null)
                 {
                     enemyHealth.TakeDamage(10);
                 }
            }
        }
        }
    }
    private IEnumerator Reload()
    {
        reloadState = true;
        animator2.SetTrigger("Reload");
        audiosource.PlayOneShot(reload);
        yield return new WaitForSeconds(2.5f);
        int reason = MaxMag - NowMaxMag;
        int bulletsToReload = Mathf.Min(reason, NowReserveMag);
        NowReserveMag -= bulletsToReload;
        NowMaxMag += bulletsToReload;
        reloadState = false;
    }
    private void ShowAmmo()
    {
        NowAmmoMag.text = NowMaxMag.ToString();
        NowReserveAmmo.text = NowReserveMag.ToString();
    }
      public static void PickUpBullets(int bullets)
    {
        NowReserveMag = Mathf.Min(NowReserveMag + bullets, MaxReserveMag2);
    }
}
