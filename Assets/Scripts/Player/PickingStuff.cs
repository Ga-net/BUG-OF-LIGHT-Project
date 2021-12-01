using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingStuff : MonoBehaviour
{


    public float TimeBetTakes;

    float TakeCounter;
    bool HasTakeRecently;
    private void Update()
    {
        if(HasTakeRecently)
            TakeCounter -= Time.deltaTime;
        if (TakeCounter <= 0)
        {
            TakeCounter = TimeBetTakes;
            HasTakeRecently = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Weapon
        if (!HasTakeRecently && other.CompareTag("Weapon") && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1") ||Input.GetKeyDown(KeyCode.Joystick1Button1) ))
        {
            PickWeapon(other.gameObject);
            HasTakeRecently = true;
        }

        //Vacuum
        if (!HasTakeRecently && other.CompareTag("Vacuum") && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            PickVacuum(other.gameObject);
            HasTakeRecently = true;
        }

        //Food
        if (!HasTakeRecently && other.CompareTag("Food") && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            PickFood(other.gameObject);
            HasTakeRecently = true;
        }

        //Blue Energy Tube
        if (!HasTakeRecently && (other.CompareTag("BlueEnergyTube") || other.CompareTag("YellowEnergyTube")) && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            PickEnergyTube(other.gameObject);
            HasTakeRecently = true;
        }
    }





    void PickWeapon(GameObject Weapon)
    {
        if(MissionTow.CanTakeTheWeapon)
        {
            Destroy(Weapon);
            PlayerManager.HasWeapons = true;
            GameManager.CorentTool = GameManager.Tools.Weapon;

        } else if(!MissionTow.CanTakeTheWeapon)
        {
            Instantiate(ErrorSound, transform.position, Quaternion.identity);
        }
    }


    public GameObject ErrorSound;
    void PickVacuum(GameObject Vacuum)
    {
        if(MissionOne.CanTakeTheVacuum)
        {
            Destroy(Vacuum);
            PlayerManager.HasVacuum = true;
            GameManager.CorentTool = GameManager.Tools.FoodTube;
        }
        else if(!MissionOne.CanTakeTheVacuum)
        {
            Instantiate(ErrorSound, transform.position, Quaternion.identity);
        }
    }



    [Tooltip("The Moor Hunger The Beter is It For Player Somewhat Flip")]
    public float HungerAddAmount;
    public GameObject EatingSound;
    public AudioClip EatingClip;
    void PickFood(GameObject Food)
    {
        Destroy(Food);
        Instantiate(EatingSound, transform.position, Quaternion.identity);
        Invoke("AddHungerDelay", EatingClip.length/2);
    }

    /*for mission one */int MissionOneApples; 
    void AddHungerDelay()
    {
        PlayerManager.CorentHungerLevel += HungerAddAmount;
        if(MissionOneApples<=5)
        {
            MissionOne.MissionOneApplesCount++;
            MissionOneApples++;
        }
    }




    void PickEnergyTube(GameObject Tube)
    {
        if(Tube.CompareTag("BlueEnergyTube"))
        {
            Destroy(Tube);
            PlayerManager.BlueEnergyTubesAmount++;
        }

        if(Tube.CompareTag("YellowEnergyTube"))
        {
            Destroy(Tube);
            PlayerManager.YellowEnergyTubesAmount++;
        }
    }

}
