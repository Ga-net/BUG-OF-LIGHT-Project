using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesAlingment : MonoBehaviour
{
    Transform[] Obstacles;
    public LayerMask RayLayerMask;
    void Start()
    {
        Obstacles = GetComponentsInChildren<Transform>();

        for (int i = 0; i < Obstacles.Length; i++)
        {
            Ray ray = new Ray(Obstacles[i].position, Vector3.down * 5000);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5000, RayLayerMask) && hit.collider.CompareTag("Ground"))
            {
                Obstacles[i].position = hit.point;
                Obstacles[i].rotation = Quaternion.LookRotation(Obstacles[i].forward, hit.normal);
            }
            else
            {
                //Destroy(Obstacles[i].gameObject, 0.05f);
            }
        }

    }
    
}
