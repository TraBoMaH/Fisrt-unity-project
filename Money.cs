
using System.Runtime.CompilerServices;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Money : MonoBehaviour
{
public static int money = 1000;
public GameObject[] weapons;
public int[] weaponCosts;
public static bool[] unlockedWeapons;
public static int BaseLvl = 0;
public GameObject[] WeaponBuyButtons; // нужно чтобы удалять кнопки приобретения оружия по купленому индексу, индекс уничтожения должен быть одинаковым с открываемым оружием
public GameObject MachineGunTMP;
public GameObject ShotGunTMP;
public GameObject UziTMP;
public GameObject[] Bases;
public Transform InstantiatePosition;
public static TextMeshProUGUI moneyScore;
public TextMeshProUGUI moneyScore2;
private GameObject lastBase;
public static GameObject lastWeapon;
private NavMeshSurface surface;

private void Start() 
{
    unlockedWeapons = new bool[weapons.Length];
    moneyScore = moneyScore2;
    moneyScore.text = money.ToString();
    unlockedWeapons[0] = true;
    surface = GameObject.Find("NavMesh Surface").GetComponent<NavMeshSurface>();
    lastWeapon = weapons[0];
    Invoke("BuildOnLoad", 10f);
}
private void Update() {
    if(Input.GetKeyDown(KeyCode.Alpha1)){ Switch0(); }
    if(Input.GetKeyDown(KeyCode.Alpha2)){ Switch1(); }
    if(Input.GetKeyDown(KeyCode.Alpha3)){ Switch2(); }
    moneyScore.text = money.ToString();
}
public static void Addmoney(int value)
{
    money += value;
}
public static void MinusMoney(int value)
{
    money -= value;
}
private void UnlockAnyOf(int index)
{
    if(weaponCosts[index] <= money)
    {
        unlockedWeapons[index] = true;
        money -= weaponCosts[index];
        Destroy(WeaponBuyButtons[index]);
    }
}
private void SwitchAnyOf(int weaponIndex)
{
     if (weaponIndex >= 0 && weaponIndex < weapons.Length)
        {
            if (unlockedWeapons[weaponIndex])
            {
                // Деактивируем все оружия
                foreach (var weapon in weapons)
                {
                    weapon.SetActive(false);
                }

                // Активируем выбранное оружие
                weapons[weaponIndex].SetActive(true); 
            }
        }
    }
public void Switch0()
{
    SwitchAnyOf(0);
    ShotGunTMP.SetActive(false);
    MachineGunTMP.SetActive(true); // для отображения пуль оружия. для каждого оружия свои TextMeshPro
    UziTMP.SetActive(false);
    lastWeapon = weapons[0];
}
public void Switch1()
{
    SwitchAnyOf(1);
    ShotGunTMP.SetActive(true);
    MachineGunTMP.SetActive(false); // для отображения пуль оружия. для каждого оружия свои TextMeshPro
    UziTMP.SetActive(false);
    lastWeapon = weapons[1];
}
public void Switch2()
{
    SwitchAnyOf(2);
    ShotGunTMP.SetActive(false);
    MachineGunTMP.SetActive(false); // для отображения пуль оружия. для каждого оружия свои TextMeshPro
    UziTMP.SetActive(true);
    lastWeapon = weapons[2];
}
public void Unlock1()
{
    UnlockAnyOf(1);
}
public void Unlock2()
{
    UnlockAnyOf(2);
}
public void UpgradeBase()
{
    int a = 100 * BaseLvl * 2;
    if(a <= money)
    {
    BaseLvl ++; money -= a;
    if(lastBase != null)
    Destroy(lastBase);
    lastBase = Instantiate(Bases[BaseLvl], InstantiatePosition.transform.position, Quaternion.identity);
    surface.BuildNavMesh();
    }
    
}
public void BuyAirStrikeCount()
{
    AirStrike.PlusCount();
    money -= 50;
}
public void BuyHealFlasks()
{
    health.GetMoreFlasks(1);
    money -= 50;
}
public static void OFFLastWeapon()
{
    lastWeapon.SetActive(false);
}
public static void ONLastWeapon()
{
    lastWeapon.SetActive(true);
}
public void BuildOnLoad()
{
    Instantiate(Bases[BaseLvl], InstantiatePosition.transform.position, Quaternion.identity);
    surface.BuildNavMesh();
}
}




// логика: если мы просто имеем уже заспавненые оружия после чего разблокируем и можешь менять их состояние
// с действующего на обратное, а оружия будут "храниться в ящике"