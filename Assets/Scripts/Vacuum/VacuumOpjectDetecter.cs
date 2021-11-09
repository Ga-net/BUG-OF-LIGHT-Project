using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumOpjectDetecter : MonoBehaviour
{
    public Transform VacuumTip;
    public float ScoopingSpeed;
    public float ExplosionForcMin;
    public float ExplosionForcMax;
    public float ExplostionRadiosMin;
    public float ExplostionRadiosMax;
    public Vector3 FPMi;
    public Vector3 FPMa;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Blue EGG") || other.CompareTag("Yellow EGG") || other.CompareTag("Food"))
        {
            //فعل دالة الشفط
            if (Input.GetButton("Fire1"))
            {
                BringIt(other.transform, other.GetComponent<Rigidbody>());

            }
        }
        else if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().isKinematic == false)
        {

            if (Input.GetButton("Fire1"))
                BringIt(other.transform, other.GetComponent<Rigidbody>());
        }


        //if (other.GetComponent<Rigidbody>() != null && !(other.CompareTag("Blue EGG") || other.CompareTag("Yellow EGG") || other.CompareTag("Food")))
        //{
        //    //فعل دالة التقريب فقط أو دالة الرمي
        //    //Debug.Log("not Good");
        //}
    }



    void BringIt (Transform OBJToBring,Rigidbody OBJRig)
    {
        OBJRig.AddExplosionForce(Random.Range(ExplosionForcMin, ExplosionForcMax), OBJToBring.position + new Vector3(Random.Range(FPMi.x,FPMa.x), Random.Range(FPMi.y, FPMa.y), Random.Range(FPMi.z, FPMa.z)), Random.Range(ExplostionRadiosMin, ExplostionRadiosMax));
        OBJToBring.position = Vector3.MoveTowards(OBJToBring.position, VacuumTip.position, ScoopingSpeed);
    }
}


//كل ما يفعله هذا الكلاس هو أن يقوم بتقريب الكائنات التي بها 
//رجدبودي إلى الـفاكيوم تيب مع عمل تأثير عشوائية للرياح