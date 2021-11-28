using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject Food;
    public int MaxFoodAmount;
    public float SpawningTime;
    bool InMaxFoodAmount;

    private void Start()
    {
        StartCoroutine(MaxFoodAmountCheck());
    }

    public Vector3 MaxPos;
    public Vector3 MinPos;

    float CorentFoodSpawning;
    void Update()
    {
        CorentFoodSpawning -= Time.deltaTime;

        if (CorentFoodSpawning <= 0 && !InMaxFoodAmount)
        {
            Instantiate(Food, transform.position + new Vector3(Random.Range(MaxPos.x,MinPos.x),Random.Range(MaxPos.y,MinPos.y),Random.Range(MaxPos.z,MinPos.z)), Quaternion.identity);
            CorentFoodSpawning = SpawningTime+Random.Range(SpawningTime/4,SpawningTime*4);
        }
    }


    IEnumerator MaxFoodAmountCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            if (GameManager.FoodAmount >= MaxFoodAmount)
                InMaxFoodAmount = true;
        }
    }

}
