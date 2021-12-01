using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBug : MonoBehaviour
{
    //Light Bug Variations
    public bool IsBlue;
    public bool IsYellow;

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
            Instantiate(Egg, IncubatingPos.position + (Vector3.down * 2), Quaternion.identity);
            CorentEggIncubating = incubatingTime;
            NearestNest = null;
        }
    }



    //Health And Damage
    public float Health;
    float Health_Saver;
    bool IsDead;
    public GameObject DamageGUIObj;
    public void TakeDamage(float DamageToTake)
    {
        Health -= DamageToTake;
        Happyness -= HappynessDecreasingAmount;
        GameObject CorentDamageUI =  Instantiate(DamageGUIObj, transform.position/* + (transform.up *0)*/, Quaternion.identity);
        CorentDamageUI.GetComponentInChildren<Damage_Heal_UI>().DamageAmount(DamageToTake);

        if (Health <= 0)
        {
            if(!IsDead)
            {
                Anim.SetBool("IsDead", true);
            }
            IsDead = true;
        }
        
    }

    public GameObject HealGUIObj;

    public void TakeHeal(float HealToTake)
    {
        if(Health < Health_Saver)
        {
            Health += HealToTake;
        }
        if(Happyness < 10)
        {
            Happyness += HappynessDecreasingAmount/2;
        }
            GameObject CorentHealUI = Instantiate(HealGUIObj, transform.position/* + (transform.up *0)*/, Quaternion.identity);
            CorentHealUI.GetComponentInChildren<Damage_Heal_UI>().HealAmount(HealToTake);
    }

    public GameObject GroundHItSound;
    bool GetDestroyed;
    //public float FallingDeathVelocity;
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Incubator")) && IsDead)
        {
            if(!GetDestroyed)
            {
                GetDestroyed = true;
                Instantiate(GroundHItSound, transform.position, Quaternion.identity);
                Destroy_F();
            }
        }

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
        if(IsBlue)
        {
            WeathorManager.BlueLightBugsCount--;
        }
        if(IsYellow)
        {
            WeathorManager.YellowLightBugsCount--;//IsYellow
        }
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
                if (transform.position.y < -4000)
                    Destroy(gameObject);
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
        Defensi,//--
        TakeingFood,//--
        FlowingPlayer,
        PlayerControll
    }

    State CorentState;

    //State Chaging
    void StateChanger()
    {
        if(IsDead)
        {
            CorentState = State.Normal;
        }
        else if (CorentEggIncubating <= incubatingTime / 5)
        {
            CorentState = State.EggButing;
        }
        else if ((Vector3.Distance(PlayerTran.position, transform.position) <= PlayerDetectionRange && Happyness <= 7) || Happyness <= 4)
        {
            CorentState = State.Attack;
        }
        else if(Happyness >= 10)
        {
            CorentState = State.FlowingPlayer;
        }
        else
        {
            CorentState = State.Normal;
        }
    }



    //Norml State
    [Tooltip("Work in the z,x Only")]
    public float AirFriction;
    void Normal()
    {
        Vector3 NegativeVelocity = new Vector3(Rig.velocity.x,0,Rig.velocity.z)*-1;

        Rig.AddForce(NegativeVelocity * AirFriction * Time.deltaTime);
    }

    void velocityReduser()
    {
        if(Rig.velocity.magnitude >3)
        {
            if(Rig.velocity.x >0)
            {
                Rig.velocity -= new Vector3(Time.deltaTime, 0, 0);
            }
            if (Rig.velocity.z > 0)
            {
                Rig.velocity -= new Vector3(0, 0, Time.deltaTime);
            }
            if (Rig.velocity.x < 0)
            {
                Rig.velocity += new Vector3(Time.deltaTime, 0, 0);
            }
            if (Rig.velocity.z < 0)
            {
                Rig.velocity += new Vector3(0, 0, Time.deltaTime);
            }
        }
    }


    //Attack State
    public Transform PlayerTran;//assign in start
    public float PlayerDetectionRange;

    void attackPlayer()
    {
        GoToword(PlayerTran, Vector3.up);

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


    Vector3 PlayerFowloingOffset;

    void FlowingPlayer_F()
    {
        GoToword(PlayerTran,PlayerFowloingOffset);
    }

    public float AttackingRange;
    public float BugDamageingAmount;
    void DamagePlayer()//Call By the attack Animation
    {
        if(Vector3.Distance(PlayerTran.position, transform.position) <= AttackingRange)
        {
            PlayerManager.TakeDamage(BugDamageingAmount);
            //Debug.Log("Damage");
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
    }



    //Play / stop Animatin

    void AnimStop_Play()
    {
        if (NearestNest == null)
            Anim.SetBool("GoingToNest", false);


    }

    private void Start()
    {
        Health_Saver = Health;
        PlayerFowloingOffset = new Vector3(Random.Range(-7, 8), Random.Range(1, 5), Random.Range(-7, 8));

        if(IsBlue)
        WeathorManager.BlueLightBugsCount++;
        if(IsYellow)
        WeathorManager.YellowLightBugsCount++;



        PlayerTran = GameObject.Find("FPSController NAMISIMP").transform;
        CorentEggIncubating = incubatingTime;

        //For Randomness
        CorentEggIncubating = Random.Range(incubatingTime / 3, incubatingTime);

    }


    private void Update()
    {
        StateChanger();
        AnimStop_Play();
        FreezeRig();
        velocityReduser();

        if (!IsDead)
        {
            FloateingEffect();
            WhatIsAround();
            Incubating_F();
            
            if(CorentState == State.EggButing)
            {
                EggButing();
            }
            else if(CorentState == State.Attack)
            {
                attackPlayer();
            }
            else if(CorentState == State.FlowingPlayer)
            {
                FlowingPlayer_F();
            }
            else
            {
                Normal();
            }

        }
        
    }

    private void LateUpdate()
    {
        AntiFLip();
    }
}
