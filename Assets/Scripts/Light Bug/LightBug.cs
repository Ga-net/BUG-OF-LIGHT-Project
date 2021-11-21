using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBug : MonoBehaviour
{
    //Movement
    public float Speed;
    public float FloatingForce;
    public Rigidbody Rig;
    public float MinFloatingPos;
    public float RayDis;
    public LayerMask RayLayerMask;

    void FloateingEffect()
    {
        Ray ray = new Ray(transform.position, Vector3.down * RayDis);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,RayDis, RayLayerMask) && hit.collider.CompareTag("Ground"))
        {
            if (hit.distance < MinFloatingPos)
                Rig.AddForce(Vector3.up * 9.8f * Rig.mass * FloatingForce * Time.deltaTime);
                //Debug.Log(hit.distance);
        }

        Debug.DrawRay(transform.position, Vector3.down * RayDis);
    }




    //incubating
    public float incubatingTime;
    public float CorentEggIncubating;//Assign in start
    public GameObject Egg;
    public Transform IncubatingPos;

    void Incubating_F()
    {
        CorentEggIncubating -= Time.deltaTime;
        if (CorentEggIncubating <= 0)
        {
            Instantiate(Egg, IncubatingPos.position, Quaternion.identity);
            CorentEggIncubating = incubatingTime;
            NearestNest = null;
        }
    }



    //Health And Damage
    public float Health;
    bool IsDead;

    public void TakeDamage(float DamageToTake)
    {
        Health -= DamageToTake;
        Happyness -= HappynessDecreasingAmount;

        if (Health <= 0)
            IsDead = true;
    }



    //Happyness
    [Range(0,10)]
    public float Happyness = 5;
    [Tooltip("You Get the Dubole OF what You Type EG:1 mean Redusing by 2")]
    public float HappynessDecreasingAmount;


    //Detections
    public float DetectionRadios;
    public Collider[] ObjAround;
    public LayerMask WhatToDetect;

    void WhatIsAround()
    {
        ObjAround = Physics.OverlapBox(transform.position, Vector3.one * DetectionRadios, Quaternion.identity, WhatToDetect);

    }


    //Go to the incubator
    public LayerMask NestsLayer;
    public float IncubatTerDetectorRadios;
    public Transform NearestNest;//get reset after the incubates
    Collider[] Nests;

    void TimeForIncubates()
    {
        if (CorentEggIncubating < incubatingTime / 5)
        {
            //detect the Nearest Nest
            if(NearestNest == null)
            { 
                Nests = Physics.OverlapBox(transform.position, Vector3.one * IncubatTerDetectorRadios, Quaternion.identity, NestsLayer);
                foreach (var item in Nests)
                {
                    Debug.Log(item.name);
                }
                float[] NestsDistance = new float[Nests.Length];
                for (int i = 0; i < Nests.Length; i++)
                {
                    NestsDistance[i] = Vector3.Distance(Nests[i].transform.position, transform.position);
                }

                float NearestNestID = Mathf.Min(NestsDistance);

                for (int i = 0; i < Nests.Length; i++)
                {
                    if (NestsDistance[i] == NearestNestID)
                    {
                        NearestNest = Nests[i].transform;
                    }
                }
            }

            //Go to the neares Nest
            GoToword(NearestNest);
        }
    }



    //Go To
    public float RotationSpeed;
    void GoToword(Transform Target)//called by Other Functions
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Target.position), RotationSpeed * Time.deltaTime);
    }



    private void Start()
    {
        CorentEggIncubating = incubatingTime;
    }


    private void Update()
    {
        FloateingEffect();
        Incubating_F();
        WhatIsAround();
        TimeForIncubates();
    }
}
