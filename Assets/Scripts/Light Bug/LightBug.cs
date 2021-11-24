using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBug : MonoBehaviour
{
    //Animation
    public Animator Anim;

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
        {
            if(!IsDead)
            {
                Anim.SetBool("IsDead", true);
            }
            IsDead = true;
        }
        
    }


    //public float FallingDeathVelocity;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && IsDead)
            Destroy_F();

        //if (collision.gameObject.CompareTag("Ground") && Rig.velocity.magnitude >= FallingDeathVelocity)
        //    Destroy_F();

    }
    //Death

    //public AnimationClip DeathAnim;
    public void Destroy_F()
    {
        Destroy(gameObject/*, DeathAnim.length*/,0.15f);
    }

    private void OnDestroy()
    {
        WeathorManager.YellowLightBugsCount--;//IsYellow
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


    //Go to the incubator (Egg Butting State)
    public LayerMask NestsLayer;
    public float IncubatTerDetectorRadios;
    public Transform NearestNest;//get reset after the incubates
    Collider[] Nests;

    void EggButing()
    {
        if (CorentEggIncubating < incubatingTime / 5)
        {
            //detect the Nearest Nest
            if(NearestNest == null)
            { 
                Nests = Physics.OverlapBox(transform.position, Vector3.one * IncubatTerDetectorRadios, Quaternion.identity, NestsLayer);
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
            GoToword(NearestNest,Offset);
        }
    }



    //Go To
    public Vector3 Offset;
    public float RotateAroundSpeed;
    void GoToword(Transform Target ,Vector3 offset)//called by Other Functions
    {
        Anim.SetBool("GoingToNest", true);
        transform.position = Vector3.MoveTowards(transform.position, Target.position + offset, Speed * Time.deltaTime);
        transform.LookAt(Target.position + offset);
        transform.RotateAround(Target.position, Vector3.up, RotateAroundSpeed);
    }


    //AntiFlip

    Vector3 IdealRotation;
    public float FixRotationTime;
    void AntiFLip()
    {
        IdealRotation = new Vector3(0, transform.eulerAngles.y, 0);
        //Debug.Log("Rotation Fixeing Is Woarking");
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(IdealRotation), FixRotationTime * Time.deltaTime);
    }



    //state Machine

    public enum State
    {
        Normal,//done
        Attack,
        EggButing,//done
        Defensi,
        TakeingFood,
        FlowingPlayer,
        PlayerControll
    }

    State CorentState;



    //Norml State
    [Tooltip("Work in the z,x Only")]
    public float AirFriction;
    void Normal()
    {
        Vector3 NegativeVelocity = new Vector3(Rig.velocity.x,0,Rig.velocity.z)*-1;

        Rig.AddForce(NegativeVelocity * AirFriction * Time.deltaTime);
    }




    //Attack State
    public Transform PlayerTran;//assign in start
    public float PlayerDetectionRange;

    void attackPlayer()
    {
        if(Vector3.Distance(PlayerTran.position,transform.position) <= PlayerDetectionRange)
        {
            CorentState = State.Attack;

            GoToword(PlayerTran, Vector3.up);
        }

        if(CorentState == State.Attack)
        {
            Attacking();
        }

       
    }


   
    void Attacking()
    {
        if(Vector3.Distance(PlayerTran.position, transform.position) <= AttackingRange)
        {
            Anim.SetTrigger("EnemyIsClose");
        }
    }

    public float AttackingRange;
    public float BugDamageingAmount;
    void DamagePlayer()//Call By the attack Animation
    {
        if(Vector3.Distance(PlayerTran.position, transform.position) <= AttackingRange)
        {
            PlayerManager.TakeDamage(BugDamageingAmount);
            Debug.Log("Damage");
        }
    }


    //Add Rig Const
    void FreezeRig()
    {
        if(CorentState == State.Attack)
        {
            Rig.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            Rig.constraints = RigidbodyConstraints.None;
        }

        if(Vector3.Distance(PlayerTran.position, transform.position) >= PlayerDetectionRange)
        {
            CorentState = State.Normal;
            //????????????????????????
        }
    }



    //Play / stop Animatin

    void AnimStop_Play()
    {
        if (NearestNest == null)
            Anim.SetBool("GoingToNest", false);


    }

    private void Start()
    {
        int x = Random.Range(1, 3);//??????????????????
        if(x == 1)//??????????????????
            WeathorManager.YellowLightBugsCount++;//??????????????????
        if (x == 2)//??????????????????
            WeathorManager.BlueLightBugsCount++;//???????????????



            PlayerTran = GameObject.Find("FPSController NAMISIMP").transform;
        CorentEggIncubating = incubatingTime;
    }


    private void Update()
    {
        if (!IsDead)
        {
            FloateingEffect();
            Incubating_F();
            WhatIsAround();
            EggButing();
            AnimStop_Play();
            Normal();
            attackPlayer();
            FreezeRig();
        }
        
    }

    private void LateUpdate()
    {
        AntiFLip();
    }
}
