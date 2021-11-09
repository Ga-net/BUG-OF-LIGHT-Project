using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuboleJump : MonoBehaviour
{

    public Transform colider;
    public Transform Character;

    public float JumpCount;
    public bool TooHigh;
    public float CloseTOcharacter;

    void BringORAway (bool BringOrMoveAway)
    {
        if(!BringOrMoveAway)
        {
            colider.position = transform.position + new Vector3(5, -100, 5);
        }

        if(BringOrMoveAway)
        {
            colider.position = Character.position + new Vector3(0, CloseTOcharacter, 0);
        }
    }


    private void Start()
    {
        StartCoroutine(MoveAway());
    }
    private void Update()
    {
        transform.position = Character.position;

        if(Input.GetButtonDown("Jump") && !TooHigh)
        {
            JumpCount++;
        }

        if(JumpCount == 2)
        {
            BringORAway(true);
            Debug.Log("Second JUmp");
        }
    }

    IEnumerator MoveAway ()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            BringORAway(false);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate(); 
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            //if (!TooHigh)
            if (JumpCount >= 2)
                JumpCount = 0;
            Debug.Log("the timer Move");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")/* || other.CompareTag("Dubole Jump")*/)
        {
            Debug.Log("not Now");
        }
        else
        {
            if(other.CompareTag("Dubole Jump") && other.transform.position == transform.position)
            JumpCount = 0;
        }


        if (!(other.CompareTag("Player") || other.CompareTag("Dubole Jump")))
        {
            JumpCount = 0;
            TooHigh = false;
            Debug.Log("I hit Somthing Except Palayer and collider");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dubole Jump"))
            TooHigh = true;
    }

    //bool InGround;
    //public Transform CharPos;
    //public Transform Colider;
    //Vector3 offset;
    //public float JumpsCount;

    //void Start()
    //{
    //    InGround = true;
    //    offset = new Vector3(5, 0, 0);
    //    StartCoroutine(MoveColiderFromUnder());
    //}

    //void Update()
    //{
    //    transform.position = CharPos.position;


    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        JumpsCount++;
    //    }

    //    if(Input.GetButtonDown("Jump") && JumpsCount == 2)
    //    {
    //        //bring the collider under the character
    //        BringCollider();
    //    }

    //    if(JumpsCount >=2)
    //        Colider.position = offset;
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Dubole Jump" || collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Not now");
    //    }
    //    else
    //        JumpsCount = 0;
    //}

    //void BringCollider()
    //{
    //    Colider.position = transform.position;
    //}


    //IEnumerator MoveColiderFromUnder()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForFixedUpdate();
    //        yield return new WaitForFixedUpdate();
    //        yield return new WaitForFixedUpdate();
    //        yield return new WaitForFixedUpdate();
    //        yield return new WaitForFixedUpdate();
    //        Colider.position = offset;
    //    }
    //}
}
