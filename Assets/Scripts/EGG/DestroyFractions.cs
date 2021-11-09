using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFractions : MonoBehaviour
{
    Rigidbody childDetection;
    private void Start()
    {
        StartCoroutine(childDetectionRoten());
    }
    IEnumerator childDetectionRoten()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            childDetection = gameObject.GetComponentInChildren<Rigidbody>();
            if (childDetection == null)
                Destroy(gameObject);
        }
    }
}
