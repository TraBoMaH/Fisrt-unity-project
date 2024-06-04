using UnityEngine; 
using System.Collections;
using TMPro;

public class PanelBehavior : MonoBehaviour
{
public GameObject panel;
public GameObject tooff;
public GameObject WeaponPanel;
private void Update() 
{

    if(Input.GetKeyDown(KeyCode.Tab))
    {
        Open();
    }
    if(Input.GetKeyDown(KeyCode.Backspace))
    {
        Leave();
    }
}
public void Open()
{
    panel.SetActive(true);
    tooff.SetActive(false);
    FirstPersonLook Comp = GetComponentInChildren<FirstPersonLook>();
    Comp.enabled = false; Cursor.lockState = CursorLockMode.Confined;
    Money.OFFLastWeapon();
}
public void Leave()
{
panel.SetActive(false);
tooff.SetActive(true);
FirstPersonLook Comp = GetComponentInChildren<FirstPersonLook>();
Comp.enabled = true; Cursor.lockState = CursorLockMode.Locked;
Money.ONLastWeapon();
}
public void OpenWeaponPanel()
{
    WeaponPanel.SetActive(true);
}

public void CloseWeaponPanel()
{
    WeaponPanel.SetActive(false);
}
} 