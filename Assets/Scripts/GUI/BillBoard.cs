using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    public bool HasLine;

    Transform Cam;

    void Start()
    {
        Cam = Camera.main.transform;

        if(GetComponent<LineRenderer>() != null)
        {
            LineRen = GetComponent<LineRenderer>();
        }
    }

    public GameObject Tarrget;

    void Update()
    {
        if ((HasLine && Tarrget == null) || !(GameManager.CorentTool == GameManager.Tools.Nothing))
            Destroy(gameObject);

        transform.LookAt(transform.position + Cam.forward);

        if(HasLine && Tarrget != null)
        {
            MakeLine(Tarrget.transform);
        }
        Floating();

    }

    public Vector3 Offset;
    //Line Form The Object To the Targget
    Vector3[] TowPints = new Vector3[2];
    LineRenderer LineRen;
    void MakeLine(Transform Target)
    {
        TowPints[0] = transform.position + Offset;
        TowPints[1] = Target.position;
        LineRen.SetPositions(TowPints);
    }


    public float UpowardMovement;
    // Floating
    void Floating()
    {
        Vector3 NewPos = new Vector3(transform.localPosition.x,Mathf.Sin(Time.time) * UpowardMovement, transform.localPosition.z);
        transform.localPosition = NewPos;
    }
}
