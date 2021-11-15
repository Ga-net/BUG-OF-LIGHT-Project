using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class WeathorManager : MonoBehaviour
{
    public float TempChangingTime;
    public float PlayerDamgingTime;
    public static float Temperature = 25;
    public static int BlueLightBugsCount;
    public static int YellowLightBugsCount;
    public float OutsideTemp;
    public float DeltaChangeDivider;
    float DeltaTempChange;
    public float PlayerDamegingAmount;


    public int NormalZone, CooldZone, HotZone, FreezingZone, HeatingZone;

    public enum WeathorState
    {
        Normal,
        Coold,
        Hot,
        Freezing,
        Heating
    }

    public static WeathorState CorentWeathorState;



    void DeltaTempCalculator()//Call in Update (Done)
    {
        DeltaTempChange = Mathf.Abs(BlueLightBugsCount + OutsideTemp - YellowLightBugsCount) / DeltaChangeDivider;
    }



    void AddDeltaChangeToTemp()//Call in corotin (Done)
    {
        if (BlueLightBugsCount + OutsideTemp > YellowLightBugsCount)
        {
            Temperature -= DeltaTempChange;
        }
        else if (YellowLightBugsCount > BlueLightBugsCount + OutsideTemp)
        {
            Temperature += DeltaTempChange;
        }
    }


    void WeathorStateChanger()//Call in update (Done)
    {
        if (Temperature < HotZone && Temperature > CooldZone)
            CorentWeathorState = WeathorState.Normal;
        else if (Temperature < CooldZone && Temperature > FreezingZone)
            CorentWeathorState = WeathorState.Coold;
        else if (Temperature <= FreezingZone)
            CorentWeathorState = WeathorState.Freezing;
        if (Temperature > HotZone && Temperature < HeatingZone)
            CorentWeathorState = WeathorState.Hot;
        else if (Temperature >= HeatingZone)
            CorentWeathorState = WeathorState.Heating;
    }


    void DamageThePlayer()//Call on it's one corotine (fone)
    {
        if(CorentWeathorState == WeathorState.Heating || CorentWeathorState == WeathorState.Freezing)
        {
            //call the Player GivDamage Function And Giv it the value of the VAR PlayerDamegingAmount 
        }
    }



    public Transform HotTempVolum;
    public Transform CooldTempVolum;

    void PostProcessingEffects()// call in update
    {
        Vector3 CamPos = Camera.main.transform.position;

        if (Temperature > 0 && Temperature < 25)
            CooldTempVolum.position = new Vector3(CamPos.x, Temperature, CamPos.z);
        if (Temperature < 50 && Temperature > 25)
        {
            float x = Mathf.Abs(((Temperature - 25) / 25) - 1) * Temperature;
            HotTempVolum.position = new Vector3(CamPos.x, x, CamPos.z);
        }

        if(Temperature <=0)
            CooldTempVolum.position = CamPos;
        if (Temperature >= 50)
            HotTempVolum.position = CamPos;
    }


    private void Start()
    {
        StartCoroutine(PlayerDamigingTimer());
        StartCoroutine(WeathorTimer());
    }

    private void Update()
    {
        DeltaTempCalculator();
        WeathorStateChanger();
        PostProcessingEffects();
    }

    



    IEnumerator PlayerDamigingTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(PlayerDamgingTime);
            if(CorentWeathorState == WeathorState.Freezing || CorentWeathorState == WeathorState.Heating)
                DamageThePlayer();
        }
    }


    IEnumerator WeathorTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(TempChangingTime);
            AddDeltaChangeToTemp();
        }
    }

}
