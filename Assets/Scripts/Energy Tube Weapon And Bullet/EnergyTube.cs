using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTube : MonoBehaviour
{
    public bool IsBlue;
    public bool IsYellow;
    public int EnergyAmount;
    public float ThrowingForc;

    //Graphics
    public Transform TubeParticleStoper;


    Weapon BlueWeapon;
    Weapon YellowWeapon;
    private void Start()
    {
        if(GameObject.Find("Blue Weapon NAMISIMP")!= null)
        BlueWeapon = GameObject.Find("Blue Weapon NAMISIMP").GetComponent<Weapon>();
        if(GameObject.Find("Yellow Weapon NAMISIMP") != null)
        YellowWeapon = GameObject.Find("Yellow Weapon NAMISIMP").GetComponent<Weapon>();
    }
    public void TakeEnergy()
    {
        if (IsBlue && GameManager.CorentTool == GameManager.Tools.Weapon)
            EnergyAmount = BlueWeapon.BulletsLeft;
        if (IsYellow && GameManager.CorentTool == GameManager.Tools.Weapon)
            EnergyAmount = YellowWeapon.BulletsLeft;
    }


    bool GetThrowed;
    void ThrowTheTube()
    {
        if(!GetThrowed)
        {
            if(IsBlue)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(transform.right * ThrowingForc, transform.position + new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), ForceMode.Acceleration);
                GetThrowed = true;
            }
            if(IsYellow)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-1 * transform.right * ThrowingForc, transform.position + new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f)), ForceMode.Acceleration);
                GetThrowed = true;
            }
        }
    }


    public float Pos5, Pos4, Pos3, Pos2, Pos1;

    void GraphicChanger()
    {
        switch (EnergyAmount)
        {
            case 1:
                TubeParticleStoper.localPosition = new Vector3(0, 0, Pos1);
                break;
            case 2:
                TubeParticleStoper.localPosition = new Vector3(0, 0, Pos2);
                break;
            case 3:
                TubeParticleStoper.localPosition = new Vector3(0, 0, Pos3);
                break;
            case 4:
                TubeParticleStoper.localPosition = new Vector3(0, 0, Pos4);
                break;
            case 5:
                TubeParticleStoper.localPosition = new Vector3(0, 0, Pos5);
                break;
            default:TubeParticleStoper.localPosition = new Vector3(0, 0, 1);
                break;
        }
    }





    bool IsDestroying;
    private void Update()
    {
        if(EnergyAmount <=0)
        {
            ThrowTheTube();
            
            
            //
            if(!IsDestroying)
            {
                if (IsBlue)
                    PlayerManager.BlueEnergyTubesAmount--;
                if (IsYellow)
                    PlayerManager.YellowEnergyTubesAmount--;
                Destroy();
            }
        }
        GraphicChanger();
        TakeEnergy();

    }


    void Destroy()
    {
        if (IsBlue)
            Destroy(gameObject, BlueWeapon.ReloadTime);
        if (IsYellow)
            Destroy(gameObject, YellowWeapon.ReloadTime);
        IsDestroying = true;
    }
}
