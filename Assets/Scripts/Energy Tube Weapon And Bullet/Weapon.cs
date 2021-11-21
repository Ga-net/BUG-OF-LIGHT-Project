﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //forGun Varients
    public bool isBlue;
    public bool isYellow;

    //Bullet
    public GameObject Bullet;
    public GameObject BluEnergyTube;
    public GameObject YellowEnergyTube;
    public Transform BlueTubePos;
    public Transform YellowTubePos;


    public float MovingForce, UpForce;

    //Gun
    public float ShootingDelay, Spread, ReloadTime, ShootingCoolDown;
    public int MagazineSize, BulletPerTap;
    public bool AllowButtonHold;

    public int BulletsLeft, BulletsShot;


    bool Shoting, ReadyToShoot, Reloading;

    //Cam And Fire Pos
    public Camera MainCam;
    public Transform FirePos;

    public bool AllowToInvoke = true;

    //Graphics
    public GameObject MuzzleFlash;
    //Gui
    //-



    private void Awake()
    {
        BulletsLeft = MagazineSize;
        ReadyToShoot = true;
    }


    private void Update()
    {
        MyInput();
    }


    void MyInput()
    {
        if (AllowButtonHold)
        {
            if(isBlue)
                Shoting = Input.GetButton("Fire2");
            if (isYellow)
                Shoting = Input.GetButton("Fire1");
        }
        else
        {
            if(isBlue)
                Shoting = Input.GetButtonDown("Fire2");
            if (isYellow)
                Shoting = Input.GetButtonDown("Fire1");
        }

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && BulletsLeft < MagazineSize && !Reloading)
            Reload_F();
        //Auto Reloading
        if (ReadyToShoot && Shoting && !Reloading && BulletsLeft <= 0)
            Reload_F();

        //Shoting
        if(ReadyToShoot&&Shoting&&!Reloading&&BulletsLeft>0)
        {
            BulletsShot = 0;

            Shoot();
        }
    }


    void Shoot()
    {
        ReadyToShoot = false;

        //Ray For The Mid of Screen
        Ray ray = MainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //For Collision Detection
        Vector3 TarrgetPoint;
        if (Physics.Raycast(ray, out hit) && !hit.collider.CompareTag("Player"))
            TarrgetPoint = hit.point;
        else
            TarrgetPoint = ray.GetPoint(80);

        //Calculating The Direction
        Vector3 DirecWithoutSpread = TarrgetPoint - FirePos.position;

        //cauculatin The Spread
        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);

        //The New Direction
        Vector3 DirectionwithSpread = DirecWithoutSpread + new Vector3(x, y, 0);

        //Instantiate a bullet && MuzzleFlash
        GameObject CurrentBullet = Instantiate(Bullet, FirePos.position, Quaternion.identity);
        if (MuzzleFlash != null)
            Instantiate(MuzzleFlash, FirePos.position, Quaternion.identity);
        else Debug.Log("Dont Forget the MuzzleFlash");
        //Rotating the bullet 
        CurrentBullet.transform.forward = DirectionwithSpread.normalized;

        //Adding Force
        CurrentBullet.GetComponent<Rigidbody>().AddForce(DirectionwithSpread.normalized * MovingForce, ForceMode.Impulse);
        //- 

        BulletsLeft--;
        BulletsShot++;

        //invoking the resetShot
        if(AllowToInvoke)
        {
            Invoke("ResetShot", ShootingDelay);
            AllowToInvoke = false;
        }
        //-
    }

    void ResetShot()
    {
        //reset 
        ReadyToShoot = true;
        AllowToInvoke = true;
    }

    void Reload_F()
    {
        if(isBlue && PlayerManager.BlueEnergyTubesAmount >0)
        {
            Reloading = true;
            Invoke("ReloadFinished", ReloadTime);
        }

        if(isYellow && PlayerManager.YellowEnergyTubesAmount >0)
        {
            Reloading = true;
            Invoke("ReloadFinished", ReloadTime);
        }
    }

    GameObject CorentBlueTube;
    GameObject CorentYellowTube;

    void ReloadFinished()
    {
        if(isBlue)
        {
            CorentBlueTube = Instantiate(BluEnergyTube, BlueTubePos.position, Quaternion.identity);
            BulletsLeft = MagazineSize;
            Reloading = false;
        }
        if (isYellow)
        {
            CorentYellowTube = Instantiate(YellowEnergyTube, YellowTubePos.position, Quaternion.identity);
            BulletsLeft = MagazineSize;
            Reloading = false;
        }
    }

    private void Start()
    {
        //to fix the fireing Without Tube bug
        if (isBlue && CorentBlueTube == null && PlayerManager.BlueEnergyTubesAmount >0)
        {
            ReloadFinished();
        }
        if (isYellow && CorentYellowTube == null && PlayerManager.YellowEnergyTubesAmount > 0)
        {
            ReloadFinished();
        }
    }



    private void LateUpdate()
    {

        if (CorentBlueTube != null && BulletsLeft > 0)
        {
            CorentBlueTube.transform.position = BlueTubePos.position;
            CorentBlueTube.transform.rotation = BlueTubePos.rotation;
        }
        if (CorentYellowTube != null && BulletsLeft > 0)
        {
            CorentYellowTube.transform.position = YellowTubePos.position;
            CorentYellowTube.transform.rotation = YellowTubePos.rotation;
        }

    }


    // to Hide The corent tubes when the weapon are not in use

    public void HideAll()
    {
        if (CorentBlueTube != null)
            CorentBlueTube.SetActive(false);
        if (CorentYellowTube != null)
            CorentYellowTube.SetActive(false);
    }

    public void UnHideAll()
    {
        if (CorentBlueTube != null)
            CorentBlueTube.SetActive(true);
        if (CorentYellowTube != null)
            CorentYellowTube.SetActive(true);
    }
}