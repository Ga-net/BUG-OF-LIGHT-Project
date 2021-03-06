using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsBlue;
    public bool IsYellow;

    public Rigidbody Rig;
    public GameObject Explosion;
    public LayerMask WhatCanGetHit;


    [Range(0, 1)]
    public float Bounciness;
    public bool UseGravity;


    public float ExplosionDamage;
    public float ExplosionHeal;
    public float ExplosionRadios;
    public float ExploasinForce;

    // For When To Expload
    public int MaxCollision;
    public float MaxLifeTIme;
    public bool ExploadOnTouch;

    int Collisions;
    PhysicMaterial PhysicMat;

    private void Start()
    {
        Setup();
    }


    private void Update()
    {
        if (Collisions >= MaxCollision)
            Expload();
    }



    public GameObject[] HitSoundOpj;

    void Expload()
    {
        if (Explosion != null)
            Instantiate(Explosion, transform.position, Quaternion.identity);

        //check for enemies
        Collider[] Enemies = Physics.OverlapSphere(transform.position, ExplosionRadios, WhatCanGetHit);

        foreach (var Hitable in Enemies)
        {
            //call the take Damage Function
            // For Blue
            if (IsYellow && Hitable.GetComponent<LightBug>() != null && Hitable.CompareTag("BlueLightBug"))
            {
                Hitable.GetComponent<LightBug>().TakeDamage(ExplosionDamage);
                Instantiate(HitSoundOpj[Random.Range(0, HitSoundOpj.Length)], transform.position, Quaternion.identity);
            }else if(IsBlue && Hitable.GetComponent<LightBug>() != null && Hitable.CompareTag("BlueLightBug"))
            {
                Hitable.GetComponent<LightBug>().TakeHeal(ExplosionHeal);
            }

            //For Yellow
            if (IsBlue && Hitable.GetComponent<LightBug>() != null && Hitable.CompareTag("YellowLightBug"))
            {
                Hitable.GetComponent<LightBug>().TakeDamage(ExplosionDamage);
                Instantiate(HitSoundOpj[Random.Range(0, HitSoundOpj.Length)], transform.position, Quaternion.identity);
            }else if(IsYellow && Hitable.GetComponent<LightBug>() != null && Hitable.CompareTag("YellowLightBug"))
            {
                Hitable.GetComponent<LightBug>().TakeHeal(ExplosionHeal);
            }



            if (Hitable.GetComponent<Rigidbody>() != null)
            {
                Hitable.GetComponent<Rigidbody>().AddExplosionForce(ExploasinForce, transform.position, ExplosionRadios);
            }
        }

        Invoke("Destroy", 0.005f);

    }


    void Destroy()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //For blue
        Collisions++;
        if(IsYellow && collision.gameObject.CompareTag("BlueLightBug"))
            Expload();
        //For Yellow
        if (IsBlue && collision.gameObject.CompareTag("YellowLightBug"))
            Expload();

        if (collision.collider.CompareTag("CanHit") && ExploadOnTouch)
            Expload();
    }



    public GameObject ChargingSound;
    public GameObject BlueReactorCargingVFX;
    public GameObject YellowReactorCargingVFX;
    public float ReactorEnergyFill;


    //for Mission Four
    int MissFourBlue;
    int MissFourYellow;
    private void OnTriggerEnter(Collider other)
    {
        //For blue
        if (IsBlue && other.CompareTag("BlueReactor"))
        {
            Reactor.BlueReacEnergy += ReactorEnergyFill;
            Instantiate(ChargingSound, transform.position, Quaternion.identity);
            //Instantiate(BlueReactorCargingVFX, other.GetComponentInParent<Transform>().position,Quaternion.identity);
            //Debug.Log(Reactor.BlueReacEnergy);

            //Mission Four
            if(MissFourBlue <5)
            {
                MissionFour.MissionFourBlueReactor += 0.2f;
                MissFourBlue++;
            }

        }

        //For Yellow
        if (IsYellow && other.CompareTag("YellowReactor"))
        {
            Reactor.YellowReacEnergy += ReactorEnergyFill;
            Instantiate(ChargingSound, transform.position, Quaternion.identity);
            //Instantiate(YellowReactorCargingVFX, other.GetComponentInParent<Transform>().position, Quaternion.identity);
            //Debug.Log(Reactor.YellowReacEnergy);

            //mission Four
            if(MissFourYellow <5)
            {
                MissionFour.MissionFourYellowReactor += 0.2f;
                MissFourYellow++;
            }
        }

    }


    void Setup()
    {
        PhysicMat = new PhysicMaterial();
        PhysicMat.bounciness = Bounciness;
        PhysicMat.frictionCombine = PhysicMaterialCombine.Minimum;
        PhysicMat.bounceCombine = PhysicMaterialCombine.Maximum;

        //assign To the Collider
        GetComponent<BoxCollider>().material = PhysicMat;

        Rig.useGravity = UseGravity;
    }



}
