using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Dash : MonoBehaviour
{


    //CharacterController CharControlll;
    //public float dashspeed;
    //Vector3 DashDirection;
    //private void Start()
    //{
    //    CharControlll = GetComponent<CharacterController>();
    //}


    //private void Update()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");

    //    DashDirection = new Vector3(x, 0, z);
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        if (DashDirection.magnitude > 0.1f)
    //            CharControlll.Move(new Vector3(transform.forward.x * DashDirection.x, 0, transform.right.z * DashDirection.z) * Time.deltaTime * dashspeed);
    //        else
    //            CharControlll.Move(transform.forward * Time.deltaTime * dashspeed);
    //    }
    //}





    public FirstPersonController CharContrScript;
    public float DashMultiplayer;
    bool InDashmod;
    public float DashAmount;
    float DashAmountSaver;
    float DashIncrecAmount;
    public float DashDecreacAmount;
    public float DashCoolDwonDivider;
    public float DashIncTimer;

    // Efects 
    public Camera Cam;
    public float FOVValu;
    float FOVValuSaver;

    private void Start()
    {
        FOVValuSaver = Cam.fieldOfView;
        DashIncrecAmount = DashAmount;
        DashAmountSaver = DashAmount;
        StartCoroutine(DashIncreser());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && DashAmount >= (DashAmountSaver / DashCoolDwonDivider) && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f))
        {
            IncOrDec(DashMultiplayer, true);
            InDashmod = true;
            DashAmount -= (DashDecreacAmount * Time.deltaTime);
            if (DashAmount <= (DashAmountSaver / DashCoolDwonDivider))
                DashAmount = 0;
            if((FOVValuSaver*FOVValu) > Cam.fieldOfView && (Mathf.Abs(Input.GetAxis("Horizontal")) >0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f))
            Cam.fieldOfView += FOVValu;
            Debug.Log("IM dashing");
        }
        else 
        {
            IncOrDec(DashMultiplayer, false);
            InDashmod = false;
            if(Cam.fieldOfView > FOVValuSaver)
            Cam.fieldOfView -= FOVValu;
            Debug.Log("IM Not dashing");
        }

    }


    void IncOrDec(float IncAmo, bool IsInc)
    {
        if (IsInc && !InDashmod)
        {
            CharContrScript.m_RunSpeed *= (IncAmo * 2);
            CharContrScript.m_WalkSpeed *= IncAmo;
        }

        if (!IsInc && InDashmod)
        {
            CharContrScript.m_RunSpeed /= (IncAmo * 2);
            CharContrScript.m_WalkSpeed /= IncAmo;
        }
    }


    IEnumerator DashIncreser()
    {
        while (true)
        {
            yield return new WaitForSeconds(DashIncTimer);
            if (DashAmount < DashAmountSaver)
            {
                DashAmount += DashIncrecAmount;

                if (DashAmount > DashAmountSaver)
                    DashAmount = DashAmountSaver;
            }
        }
    }
}
