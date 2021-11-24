using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAble : MonoBehaviour
{
    public bool IsVacuum, IsWeapon, IsFood, IsBlueEnergyTube, IsYellowEnergyTube;

    public GameObject EToPickCanvas;
    public Vector3 Offset;
    GameObject CorentCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PickDetector"))
        {
            if (CorentCanvas == null)
                CorentCanvas = Instantiate(EToPickCanvas, transform.position + Offset, Quaternion.identity);

            if (CorentCanvas.GetComponentInChildren<BillBoard>() != null)
            {
                CorentCanvas.GetComponentInChildren<BillBoard>().Tarrget = gameObject;
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PickDetector"))
        {
            if (CorentCanvas != null)
                Destroy(CorentCanvas);
        }
    }

    public float HealingAmount;
    public float HungerAddAmount;
    private void OnDestroy()
    {
        if (IsVacuum)
            PlayerManager.HasVacuum = true;
        if (IsWeapon)
            PlayerManager.HasWeapons = true;
        if (IsFood)
        {
            PlayerManager.TakeFood(HungerAddAmount);
            PlayerManager.TakeHeal(HealingAmount);
        }
        if (IsBlueEnergyTube)
            PlayerManager.BlueEnergyTubesAmount++;
        if (IsYellowEnergyTube)
            PlayerManager.YellowEnergyTubesAmount++;


    }

}
