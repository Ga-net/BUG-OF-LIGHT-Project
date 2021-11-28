using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    void Start()
    {
        GameManager.FoodAmount++;
        Debug.Log(GameManager.FoodAmount);
    }

    private void OnDestroy()
    {
        GameManager.FoodAmount--;
    }
}
