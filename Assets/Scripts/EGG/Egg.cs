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

    enum EggPlace
    {
        inIncubator,
        inLap,
        inTube,
        inVoid
    }

    EggPlace CorentPlace;

    public GameObject BOLPrefab;

    public GameObject EGGFractions;


    bool IsDestroing;

    void Start()
    {
        CorentPlace = EggPlace.inVoid;
        StartCoroutine(ReducEnergy());
        StartCoroutine(IncubatesTime());
    }

    void Update()
    {
        if (IncubateTimer <= 0 && EnergyAmount > Random.Range(70, 80) && IsDestroing == false)
        {
            Instantiate(BOLPrefab, transform.position, Quaternion.identity);
        }

        if ((IncubateTimer <= 0 || EnergyAmount <= 0) && IsDestroing == false)
            DestroyEGG();

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

    IEnumerator ReducEnergy()
    {
        while(CorentPlace == EggPlace.inVoid)
        {
            yield return new WaitForSeconds(1);
            EnergyAmount -= EnergyRedeucingAmount;
            if (EnergyAmount < 0)
                EnergyAmount = 0;
        }
    }

    IEnumerator IncubatesTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            IncubateTimer--;
            if (IncubateTimer < 0)
                IncubateTimer = 0;
            //Debug.Log(IncubateTimer);
            Debug.Log(CorentPlace);
        }
    }

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

    public LAP BlueLap;

    public LAP YellowLap;

    public void TakeEnergy (float DecresAmo ,bool isIn)
    {
        BlueLap = GameObject.Find("Blue Lap Holder NAMISIMP").GetComponent<LAP>();
        YellowLap = GameObject.Find("Yellow Lap Holder NAMISIMP").GetComponent<LAP>();
        TakingEnergyAmount = DecresAmo;
        isTaking = isIn;

        if (isIn)
            StartCoroutine(TakingEnergyTimer());
        else if (!isIn)
            StopCoroutine(TakingEnergyTimer());   
    }


    IEnumerator TakingEnergyTimer()
    {
        while (isTaking)
        {
            yield return new WaitForSeconds(1);
            EnergyAmount -= TakingEnergyAmount;
            BlueLap.addEnergToLap(TakingEnergyAmount);
        }
    }


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
