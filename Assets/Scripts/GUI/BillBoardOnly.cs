using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardOnly : MonoBehaviour
{
    Transform Cam;
    void Start()
    {
        Cam = Camera.main.transform;
        Debug.Log(Cam.name);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + Cam.forward);
    }
}
