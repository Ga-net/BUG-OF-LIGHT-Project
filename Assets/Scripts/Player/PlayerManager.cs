using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int BlueEnergyTubesAmount = 5;
    public static int YellowEnergyTubesAmount = 5;
    public static bool HasVacuum = true;
    public static bool HasWeapons = false;


    //Simple Enventro System


    //Vacuum Opj
    public GameObject[] VacuumParts;

    //Weapons Opj
    public GameObject[] WeaponsParts;

    //Detection Obj (For Picking Stuff)
    public GameObject[] DetectionParts;


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

    void HideDetectors()
    {
        foreach (var item in DetectionParts)
        {
            item.SetActive(false);
        }
    }

    void ShowDetectors()
    {
        foreach (var item in DetectionParts)
        {
            item.SetActive(true);
        }
    }

    void ShowCorentSelection()
    {
        switch (GameManager.CorentTool)
        {
            case GameManager.Tools.Nothing: HideVacuum(); HideWeapons(); ShowDetectors();
                break;
            case GameManager.Tools.Vacuum: if (HasVacuum) { HideWeapons(); ShowVacuum();} HideDetectors();
                break;
            case GameManager.Tools.Weapon: if (HasWeapons) { HideVacuum(); ShowWeapons();} HideDetectors();
                break;
            case GameManager.Tools.BlueEggTube: if (HasVacuum) { HideWeapons(); ShowVacuum();} HideDetectors();
                break;
            case GameManager.Tools.YellowEggTube: if (HasVacuum) { HideWeapons(); ShowVacuum();} HideDetectors();
                break;
            case GameManager.Tools.FoodTube:if (HasVacuum) { HideWeapons(); ShowVacuum();} HideDetectors();
                break;
            default:HideVacuum(); HideWeapons(); HideDetectors();
                break;
        }
    }


    // Player health
    public static float Health;
    public float Player_Health;
    
    //bool IsDead;

    public static void TakeDamage(float DamageToTake)
    {
        Health -= DamageToTake;
    }

    public static void TakeHeal(float HealingToTake)
    {
        Health += HealingToTake;
        //NoBassLimet(); // Call In Update
    }
    void NoBassLimet()
    {
        //For Healing
        if (Health >= Player_Health)
            Health = Player_Health;
        //For Hunger
        if (CorentHungerLevel >= Player_Hunger)
            CorentHungerLevel = Player_Hunger;
    }


    //Hunger
    //public static float Hunger;
    public float Player_Hunger;
    public static float CorentHungerLevel;

    void PlayerHunger()
    {
        CorentHungerLevel -= Time.deltaTime;

        if(CorentHungerLevel <= 0)
        {
            //call the death Function
        }
    }

    public static void TakeFood(float FoodAmount)
    {
        CorentHungerLevel += FoodAmount;
    }


    //GUI
    public Text BlueEnergyTubes;
    public Text YellowEnergyTubes;
    public Animator GuiAnimator;
    public Animator GuiAnimator2;

    bool TabIsHold;
    public GameObject SlideSound;
    void MoreContentSlider()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabIsHold = !TabIsHold; 
            GuiAnimator.SetBool("TabIsHold", TabIsHold);
            GuiAnimator2.SetBool("TabIsHold", TabIsHold);
            Instantiate(SlideSound);
        }
    }


    void UpdateTubesCountUI()
    {
            BlueEnergyTubes.text = BlueEnergyTubesAmount.ToString();
            YellowEnergyTubes.text = YellowEnergyTubesAmount.ToString();
    }


    //States GUI
    //1- Health
    public Image HealthBarFill;
    //2- Hunger
    public Image HungerBarFill;

    //Vacuum
    public VacuumTip VacuumScript;

    //3- Food In Vacuum
    public Image VacuumFoodFill;
    //4- Blue Eggs In Vacuum
    public Image BlueEggesFill;
    //5- Yellow Eggs In Vacuum
    public Image YellowEggsFill;


    void UpdateBars()
    {
        //Health
        HealthBarFill.fillAmount = Health / Player_Health;
        //Hunger
        HungerBarFill.fillAmount = CorentHungerLevel / Player_Hunger;

        //Vacuum

        if(VacuumScript != null)
        {
            //Food
            if(VacuumFoodFill != null)
            {
                VacuumFoodFill.fillAmount = (float)VacuumScript.Food.Count / (float)VacuumScript.TubeMax;
                //Blue Eggs
                BlueEggesFill.fillAmount = (float)VacuumScript.BlueEggs.Count / (float)VacuumScript.TubeMax;
                //Yellow
                YellowEggsFill.fillAmount = (float)VacuumScript.YellowEggs.Count / (float)VacuumScript.TubeMax;
            }
        }
    }



    private void Start()
    {
        Health = Player_Health;
        //Hunger = Player_Hunger;
        CorentHungerLevel = Player_Hunger;
    }


    private void Update()
    {
        ShowCorentSelection();
        PlayerHunger();
        NoBassLimet();//To make Shure the Hunger And the Healing is No More than the Max
        //GUI
        UpdateTubesCountUI();
        MoreContentSlider();
        UpdateBars();
    }

}
