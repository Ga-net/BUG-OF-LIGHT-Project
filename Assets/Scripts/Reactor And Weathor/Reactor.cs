using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Reactor : MonoBehaviour
{
    //Effects 
    public Slider BlueSlider;
    public Slider YellowSlider;


    public static float BlueReacEnergy = 0;
    public static float YellowReacEnergy = 0;

    public float TempreatureChangingTime;
    public float EnergyRedusingAmount;
    public float EnergeToTemperatureDevider;
    public float MaxEnergyAmount;
    public Animator ReactorAnimator;


    public GameObject BLueSpredingEffect;
    //public Transform BLueSpredingEffectPOS;
    public GameObject YellowSpredingEffect;
    //public Transform YellowSpredingEffectPOS;

    public TMP_Text BlueReactorText;
    public TMP_Text YellowReactorText;


    private void Start()
    {
        StartCoroutine(TemperatureChanger());
    }


    public GameObject ReactorDonwSound;
    public void SpredBlueParticleInTube()
    {
        if (BlueReacEnergy > 100)
        {
            Instantiate(BLueSpredingEffect, transform.position, transform.rotation);
            Instantiate(ReactorDonwSound, transform.position, Quaternion.identity);
        }
        Debug.Log("Blue Spreding");
    }

    public void SpredYellowParticleInTube()
    {
        if (YellowReacEnergy > 100)
        {
            Instantiate(YellowSpredingEffect, transform.position, transform.rotation);
            Instantiate(ReactorDonwSound, transform.position, Quaternion.identity);
        }
        Debug.Log("Yellow Spreding");
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
            BlueReactorText.text = BlueReacEnergy.ToString()/*.Insert(BlueReacEnergy.ToString().Length, "/").Insert(BlueReacEnergy.ToString().Length + 1, MaxEnergyAmount.ToString())*/;
            YellowReactorText.text = YellowReacEnergy.ToString()/*.Insert(YellowReacEnergy.ToString().Length, "/").Insert(YellowReacEnergy.ToString().Length + 1, MaxEnergyAmount.ToString())*/;
        }
    }


}
