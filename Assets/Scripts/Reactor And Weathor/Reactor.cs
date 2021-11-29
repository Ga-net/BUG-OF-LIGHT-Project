using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reactor : MonoBehaviour
{
    //Effects 
    public Slider BlueSlider;
    public Slider YellowSlider;


    public static float BlueReacEnergy = 1000;
    public static float YellowReacEnergy = 1000;

    public float TempreatureChangingTime;
    public float EnergyRedusingAmount;
    public float EnergeToTemperatureDevider;
    public float MaxEnergyAmount;
    public Animator ReactorAnimator;

    private void Start()
    {
        StartCoroutine(TemperatureChanger());
    }


    IEnumerator TemperatureChanger()//start at start
    {

        while (true)
        {
            yield return new WaitForSeconds(TempreatureChangingTime);

            if ((WeathorManager.CorentWeathorState == WeathorManager.WeathorState.Coold || WeathorManager.CorentWeathorState == WeathorManager.WeathorState.Freezing) && YellowReacEnergy >= EnergyRedusingAmount)
            {
                YellowReacEnergy -= EnergyRedusingAmount;
                WeathorManager.Temperature += EnergyRedusingAmount / EnergeToTemperatureDevider;
            }

            if ((WeathorManager.CorentWeathorState == WeathorManager.WeathorState.Hot || WeathorManager.CorentWeathorState == WeathorManager.WeathorState.Heating) && BlueReacEnergy >= EnergyRedusingAmount)
            {
                BlueReacEnergy -= EnergyRedusingAmount;
                WeathorManager.Temperature -= EnergyRedusingAmount / EnergeToTemperatureDevider;
            }


            //Update Effects
            BlueSlider.value = BlueReacEnergy;
            YellowSlider.value = YellowReacEnergy;
        }
    }


}
