using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAP : MonoBehaviour
{
    public bool YellowEGGLap;
    public bool BlueEGGLap;

    public float LapEnergyAmount;

    public float EggEnergyReducingAmont;


    //working when an Egg Enter the trigger Area
    private void OnTriggerStay(Collider other)
    {
        //For the Blue Instanse
        if (other.CompareTag("Blue EGG") && BlueEGGLap)
        {
            if (other.GetComponent<Egg>() != null)
            {
                if (!(other.GetComponent<Egg>().isTaking))
                {
                    other.GetComponent<Egg>().TakeEnergy(EggEnergyReducingAmont, true);
                }
            }
        }

        //For the Yellow Instanse
        if (other.CompareTag("Yellow EGG") && YellowEGGLap)
        {
            if (other.GetComponent<Egg>() != null)
            {
                if (!(other.GetComponent<Egg>().isTaking))
                {
                    other.GetComponent<Egg>().TakeEnergy(EggEnergyReducingAmont, true);
                }
            }
        }

        //for Throwing the Wrong Eggs

        if (other.CompareTag("Yellow EGG") && BlueEGGLap)
        {
            other.GetComponent<Egg>().ThrowItAway();
        }

        if (other.CompareTag("Blue EGG") && YellowEGGLap)
        {
            other.GetComponent<Egg>().ThrowItAway();
        }
    }


    //Use By the EGG To GIV The lap Energt
    public void addEnergToLap(float AmountToAdd)
    {
        LapEnergyAmount += AmountToAdd;
    }


    //Instantiation Section

    public float EnergyTubePrice;
    public float EnergyTubeIncreac;
    public float EnergyTubeCookTime;

    public GameObject BlueEnergyTube;
    public Transform BlueEnergyTubeSpawonPos;
    public GameObject YellowEnergyTube;
    public Transform YellowEnergyTubeSpawonPos;

    public float CorentTube;

    private void Start()
    {
        StartCoroutine(CookingTheTube());
    }

    private void Update()
    {
        if (CorentTube >= EnergyTubePrice)
            MakeATube();
    }


    IEnumerator CookingTheTube()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnergyTubeCookTime);
            if(LapEnergyAmount >= EnergyTubeIncreac)
            {
                CorentTube += EnergyTubeIncreac;
                LapEnergyAmount -= EnergyTubeIncreac;
                //Debug.Log("Timer Is Working");

            }
        }
    }


    void MakeATube()
    {
        if(CorentTube >= EnergyTubePrice && BlueEGGLap)
        {
            CorentTube = 0;
            Instantiate(BlueEnergyTube, BlueEnergyTubeSpawonPos.position,Quaternion.identity);
        }

        if (CorentTube >= EnergyTubePrice && YellowEGGLap)
        {
            CorentTube = 0;
            Instantiate(YellowEnergyTube, YellowEnergyTubeSpawonPos.position, Quaternion.identity);
        }
    }
}
