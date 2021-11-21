using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int BlueEnergyTubesAmount = 5;
    public static int YellowEnergyTubesAmount = 5;
    public static bool HasVacuum = true;
    public static bool HasWeapons = true;


    //Simple Enventro System


    //Vacuum Opj
    public GameObject[] VacuumParts;

    //Weapons Opj
    public GameObject[] WeaponsParts;



    void HideVacuum()
    {
        foreach (var item in VacuumParts)
        {
            if (item.GetComponentInChildren<VacuumTip>() != null)
                item.GetComponentInChildren<VacuumTip>().HideAll();

            item.SetActive(false);
        }
    }

    void ShowVacuum()
    {
        foreach (var item in VacuumParts)
        {
            if (item.GetComponentInChildren<VacuumTip>() != null)
                item.GetComponentInChildren<VacuumTip>().UnHideAll();

            item.SetActive(true);
        }
    }

    void HideWeapons()
    {
        foreach (var item in WeaponsParts)
        {
            if (item.GetComponent<Weapon>() != null)
                item.GetComponent<Weapon>().HideAll();

            item.SetActive(false);
        }
    }

    void ShowWeapons()
    {
        foreach (var item in WeaponsParts)
        {
            if (item.GetComponent<Weapon>() != null)
                item.GetComponent<Weapon>().UnHideAll();

            item.SetActive(true);
        }
    }

    void ShowCorentSelection()
    {
        switch (GameManager.CorentTool)
        {
            case GameManager.Tools.Nothing: HideVacuum(); HideWeapons();
                break;
            case GameManager.Tools.Vacuum: if (HasVacuum) { HideWeapons(); ShowVacuum(); }
                break;
            case GameManager.Tools.Weapon: if (HasWeapons) { HideVacuum(); ShowWeapons(); }
                break;
            case GameManager.Tools.BlueEggTube: if (HasVacuum) { HideWeapons(); ShowVacuum(); }
                break;
            case GameManager.Tools.YellowEggTube: if (HasVacuum) { HideWeapons(); ShowVacuum(); }
                break;
            case GameManager.Tools.FoodTube:if (HasVacuum) { HideWeapons(); ShowVacuum(); }
                break;
            default:HideVacuum(); HideWeapons();
                break;
        }
    }


    private void Update()
    {
        ShowCorentSelection();
    }

}
