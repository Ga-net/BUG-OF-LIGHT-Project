using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int BlueEnergyTubesAmount;
    public static int YellowEnergyTubesAmount;
    public static bool HasVacuum = false;
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

    public static bool IsDead;

    public static void TakeDamage(float DamageToTake)
    {
        Health -= DamageToTake;
        if (Health <= 0)
            IsDead = true;
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
            Death();
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


    //death
    public enum DeathCose
    {
        Cold,
        Heat,
        Starvation,//Of not from
        Light_Bugs,
    }

    public DeathCose CorentDeathCose;

    void ChangeDeathCose()
    {
        if(Health <= 0 || CorentHungerLevel <=0)
        {
            if (Health <= 0 && WeathorManager.CorentWeathorState == WeathorManager.WeathorState.Freezing)
                CorentDeathCose = DeathCose.Cold;
            else if (Health <= 0 && WeathorManager.CorentWeathorState == WeathorManager.WeathorState.Heating)
                CorentDeathCose = DeathCose.Heat;
            else if (CorentHungerLevel <= 0)
                CorentDeathCose = DeathCose.Starvation;
            else
                CorentDeathCose = DeathCose.Light_Bugs;
        }
    }


    public GameObject[] DeathOBJToHide;
    public FirstPersonController Controlle_r;
    public GameObject DeatCanvas;
    void Death()
    {
        foreach (var item in DeathOBJToHide)
        {
            item.SetActive(false);
        }
        Controlle_r.enabled = false;
        DeatCanvas.SetActive(true);
    }



    public void TryAgainOrExit()
    {
        if (IsDead && Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (IsDead && Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

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

        //death  
        ChangeDeathCose();
        if (IsDead)
        {
            Death();
            TryAgainOrExit();
            gameObject.GetComponentInChildren<Rigidbody>().AddExplosionForce(200, gameObject.GetComponentInChildren<Rigidbody>().transform.position, 100);
        }
    }

}
