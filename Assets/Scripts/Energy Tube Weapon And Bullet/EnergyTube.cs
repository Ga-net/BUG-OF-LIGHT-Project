using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTube : MonoBehaviour
{
    public bool IsBlue;
    public bool IsYellow;
    public int EnergyAmount;
    public float ThrowingForc;


    public void TakeEnergy(int EnergyAmount)
    {
        EnergyAmount--;
    }


    bool GetThrowed;
    void ThrowTheTube()
    {
        if(!GetThrowed)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(transform.right* ThrowingForc, transform.position+new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), ForceMode.Acceleration);
            GetThrowed = true;
        }
    }


    private void Update()
    {
        if(EnergyAmount <=0)
        {
            ThrowTheTube();
        }
    }

}
