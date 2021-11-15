using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{
    public static float BlueReacEnergy;
    public static float YellowReacEnergy;

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
        }
    }


}
