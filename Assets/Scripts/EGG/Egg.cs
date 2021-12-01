using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public bool IsBlue;
    public bool IsYellow;

    public float EnergyAmount;

    [SerializeField]
    float EnergyRedeucingAmount;

    [SerializeField]
    float IncubateTimer;

    public enum EggPlace
    {
        inIncubator,
        inLap,
        inTube,
        inVoid
    }

    public EggPlace CorentPlace;

    public GameObject BOLPrefab;

    public GameObject EGGFractions;


    bool IsDestroing;

    void Start()
    {
        CorentPlace = EggPlace.inVoid;
        //StartCoroutine(ReducEnergy());
        //StartCoroutine(IncubatesTime());
        KnowTheLaps();
        //StartCoroutine(TakingEnergyTimer());
    }

    void Update()
    {
        if (IncubateTimer <= 0 && EnergyAmount > Random.Range(70, 80) && IsDestroing == false)
        {
            Instantiate(BOLPrefab, transform.position, Quaternion.identity);
        }

        if ((IncubateTimer <= 0 || EnergyAmount <= 0) && IsDestroing == false)
            DestroyEGG();

        //To redus energy without Coroutine
        ReducEnergyInVoid();

        //no coroutine
        IncubatesTime_F();
        TakingEnergyTimer_F();

        //used On the comunocation section;
        ChangeIsTaking();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Incubator":CorentPlace = EggPlace.inIncubator;
                break;
            case "Lap":CorentPlace = EggPlace.inLap;
                break;
            case "Tube":CorentPlace = EggPlace.inTube;
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Incubator":CorentPlace = EggPlace.inVoid;
                break;
            case "Lap":CorentPlace = EggPlace.inVoid;
                break;
            case "Tube":
                CorentPlace = EggPlace.inVoid;
                break;
        }
    }


    float ReducingTimer = 1;
    void ReducEnergyInVoid()
    {
        ReducingTimer -= Time.deltaTime;
        if(ReducingTimer <=0)
        {
            if (CorentPlace == EggPlace.inVoid)
                EnergyAmount -= EnergyRedeucingAmount;
            ReducingTimer = 1;
        }
    }


    //IEnumerator ReducEnergy()
    //{
    //    while(true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        if(CorentPlace == EggPlace.inVoid)
    //            EnergyAmount -= EnergyRedeucingAmount;
    //        if (EnergyAmount < 0)
    //            EnergyAmount = 0;
    //    }
    //}

    float IncubatesTime_Num = 1;
    void IncubatesTime_F()
    {
        IncubatesTime_Num -= Time.deltaTime;
        if(IncubatesTime_Num <=0)
        {

            if (!(CorentPlace == EggPlace.inTube))
                IncubateTimer--;
            if (IncubateTimer < 0)
                IncubateTimer = 0;

            IncubatesTime_Num = 1;
        }
    }


    //IEnumerator IncubatesTime()
    //{
    //    while(true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        if (!(CorentPlace == EggPlace.inTube))
    //            IncubateTimer--;
    //        if (IncubateTimer < 0)
    //            IncubateTimer = 0;
    //        //Debug.Log(IncubateTimer);
    //        //Debug.Log(CorentPlace);
    //    }
    //}

    public void DestroyEGG()
    {
        //----
        IsDestroing = true;

        GameObject tempEggFraction = Instantiate(EGGFractions, transform.position, Quaternion.identity);
        Rigidbody[] FractionsRigs = tempEggFraction.GetComponentsInChildren<Rigidbody>();

        if(FractionsRigs.Length > 0)
        {
            foreach (var Rig in FractionsRigs)
            {
                Rig.AddExplosionForce(Random.Range(200, 400), transform.position, 1);
            }
        }

        float delay = 5;

        foreach (var rig in FractionsRigs)
        {
            Destroy(rig.gameObject, delay);
            delay += 0.01f;
        }
        Destroy(gameObject);
    }



    // Class Comunocations Section

    //Lap Section

    float TakingEnergyAmount;
    public bool isTaking;

    void ChangeIsTaking()//in Update
    {
        switch (CorentPlace)
        {
            case EggPlace.inIncubator:isTaking = false;
                break;
            //case EggPlace.inLap:isTaking = true;
                //break;
            case EggPlace.inTube:isTaking = false;
                break;
            case EggPlace.inVoid:isTaking = false;
                break;
        }
    }

    public LAP BlueLap;

    public LAP YellowLap;

    void KnowTheLaps()//in start
    {
        BlueLap = GameObject.Find("Blue Lap Holder NAMISIMP").GetComponent<LAP>();
        YellowLap = GameObject.Find("Yellow Lap Holder NAMISIMP").GetComponent<LAP>();
    }

    public void TakeEnergy (float DecresAmo ,bool isIn)
    {
        TakingEnergyAmount = DecresAmo;
        isTaking = isIn;
    }


    float TakingEnergyTimer_Num = 1;
    void TakingEnergyTimer_F()
    {
        TakingEnergyTimer_Num -= Time.deltaTime;
        if(TakingEnergyTimer_Num <= 0)
        {
            if (isTaking && IsBlue)
            {
                EnergyAmount -= TakingEnergyAmount;
                BlueLap.addEnergToLap(TakingEnergyAmount);
            }
            if (isTaking && IsYellow)
            {
                EnergyAmount -= TakingEnergyAmount;
                YellowLap.addEnergToLap(TakingEnergyAmount);
            }

            TakingEnergyTimer_Num = 1;
        }
    }


    //IEnumerator TakingEnergyTimer()//in Start
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        if(isTaking && IsBlue)
    //        {
    //            EnergyAmount -= TakingEnergyAmount;
    //            BlueLap.addEnergToLap(TakingEnergyAmount);
    //        }
    //        if (isTaking && IsYellow)
    //        {
    //            EnergyAmount -= TakingEnergyAmount;
    //            YellowLap.addEnergToLap(TakingEnergyAmount);
    //        }
    //    }
    //}


    public void ThrowItAway()
    {
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(250,500), transform.position + new Vector3(Random.Range(0, 0.1f), -0.5f, Random.Range(0, 0.1f)), Random.Range(30, 50));
        Debug.Log("Did I get Throwed");
    }




    //added Stuf

    //public float BreakVelocity;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.magnitude) > BreakVelocity)
    //        EnergyAmount = 0;
    //}
}
