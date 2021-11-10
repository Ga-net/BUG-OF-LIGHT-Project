﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumTip : MonoBehaviour
{
    public int TubeMax;
    public Transform ThroingPos;

    public List<Transform> BlueEggSlots;
    public List<Transform> YellowEggSlots;
    public List<Transform> FoodSlots;

    public List<GameObject> BlueEggs;
    public List<GameObject> YellowEggs;
    public List<GameObject> Food;


    public float ThrowingForce;
    public Vector3 ThrowingPoint;
    public float ThrowingRadios;
    public CharacterController PlayerVelocity;

    //Sounds Section
    public GameObject ThroingSound;
    public GameObject TakingSound;
    public GameObject ErrorSound;

    void PlayThrowingSound()
    {
        Instantiate(ThroingSound, ThroingPos);
    }

    void PlayTakingSound()
    {
        Instantiate(TakingSound, transform);
    }

    void PlayErrorSound()
    {
        Instantiate(ErrorSound, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        //taking the blue Eggs
        if (other.CompareTag("Blue EGG") && BlueEggs.Count < TubeMax && Input.GetButton("Fire1"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.localScale /= 2;
            other.transform.position = BlueEggSlots[BlueEggs.Count].position;
            BlueEggs.Add(other.gameObject);
            PlayTakingSound();
        }

        //taking the Yellow Eggs
        if (other.CompareTag("Yellow EGG") && YellowEggs.Count < TubeMax && Input.GetButton("Fire1"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.localScale /= 2;
            other.transform.position = YellowEggSlots[YellowEggs.Count].position;
            YellowEggs.Add(other.gameObject);
            PlayTakingSound();
        }



        //taking the Food
        if (other.CompareTag("Food") && Food.Count < TubeMax && Input.GetButton("Fire1"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.localScale /= 2;
            other.transform.position = FoodSlots[Food.Count].position;
            Food.Add(other.gameObject);
            PlayTakingSound();
        }

    }


    private void OnTriggerStay(Collider other)
    {
        //to throw the BLue Egges When the tube Is Full
        if (other.CompareTag("Blue EGG") && BlueEggs.Count > TubeMax && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
            PlayErrorSound();
        }


        //to throw the Yellow Egges When the tube Is Full
        if (other.CompareTag("Yellow EGG") && YellowEggs.Count > TubeMax && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
            PlayErrorSound();
        }



        //to throw the Food When the tube Is Full
        if (other.CompareTag("Food") && Food.Count > TubeMax && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
            PlayErrorSound();
        }



        //to throw the Other Riggids When the player Try To Cach Them
        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().isKinematic == false && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
            int Redusnois;
            Redusnois = Random.Range(0, 100);
            if(Redusnois < 5)
                PlayErrorSound();
        }
    }


    //Throw the item away if the tube is full (Universal)
    void ThrowIt(Transform OBJToThrow, Rigidbody OBJRig)
    {
        OBJRig.AddExplosionForce(Random.Range(10000,15000), OBJToThrow.position + new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), Random.Range(30, 50));
        //Debug.Log("I Throw Somthing");
    }


    //bring the item out of the tube
    //Theis function throw the lates add item and deleted from the list
    void OutOFTube()
    {
        //to bring Out a Blue Egg
        if (GameManager.CorentTool == GameManager.Tools.BlueEggTube && BlueEggs.Count > 0)
        {
            BlueEggs[BlueEggs.Count - 1].transform.position = ThroingPos.position;
            BlueEggs[BlueEggs.Count - 1].transform.rotation = Camera.main.transform.rotation;
            BlueEggs[BlueEggs.Count - 1].GetComponent<Rigidbody>().velocity = (BlueEggs[BlueEggs.Count - 1].transform.forward + (PlayerVelocity.velocity *0.05f)) * ThrowingForce;
            BlueEggs[BlueEggs.Count - 1].GetComponent<Rigidbody>().isKinematic = false;
            BlueEggs[BlueEggs.Count - 1].transform.localScale *= 2;
            BlueEggs.RemoveAt(BlueEggs.Count - 1);
            PlayThrowingSound();
        }




        //to bring Out a Yellow Egg
        if (GameManager.CorentTool == GameManager.Tools.YellowEggTube && YellowEggs.Count > 0)
        {
            YellowEggs[YellowEggs.Count - 1].transform.position = ThroingPos.position;
            YellowEggs[YellowEggs.Count - 1].transform.rotation = Camera.main.transform.rotation;
            YellowEggs[YellowEggs.Count - 1].GetComponent<Rigidbody>().velocity = (YellowEggs[YellowEggs.Count - 1].transform.forward + (PlayerVelocity.velocity * 0.05f)) *ThrowingForce;
            YellowEggs[YellowEggs.Count - 1].GetComponent<Rigidbody>().isKinematic = false;
            YellowEggs[YellowEggs.Count - 1].transform.localScale *= 2;
            YellowEggs.RemoveAt(YellowEggs.Count - 1);
            PlayThrowingSound();
        }




        //to bring Out a Food
        if (GameManager.CorentTool == GameManager.Tools.FoodTube && Food.Count > 0)
        {
            Food[Food.Count - 1].transform.position = ThroingPos.position;
            Food[Food.Count - 1].transform.rotation = Camera.main.transform.rotation;
            Food[Food.Count - 1].GetComponent<Rigidbody>().velocity = (Food[Food.Count - 1].transform.forward + (PlayerVelocity.velocity * 0.09f)) *ThrowingForce;
            Food[Food.Count - 1].GetComponent<Rigidbody>().isKinematic = false;
            Food[Food.Count - 1].transform.localScale *= 2;
            Food.RemoveAt(Food.Count - 1);
            PlayThrowingSound();
        }

    }





    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
                OutOFTube();
        }
    }






    private void LateUpdate()
    {

        //to remove The broken Blue Egges
        if (BlueEggs.Count > 0)
        {
            for (int i = 0; i < BlueEggs.Count; i++)
            {
                if(BlueEggs[i] == null)
                {
                    BlueEggs.RemoveAt(i);
                }
            }
            //for Updating The postion of Blue Eggs
            for (int i = 0; i < BlueEggs.Count; i++)
            {
                if (BlueEggSlots.Count >= i && BlueEggs.Count >= i)
                {
                    if (BlueEggSlots[i] != null && BlueEggs[i] != null)
                    {
                        BlueEggs[i].transform.position = BlueEggSlots[i].position;
                    }
                }
            }
        }



        //to remove The broken Yellow Egges
        if (YellowEggs.Count > 0)
        {
            for (int i = 0; i < YellowEggs.Count; i++)
            {
                if (YellowEggs[i] == null)
                {
                    YellowEggs.RemoveAt(i);
                }
            }
            //for Updating The postion of Blue Eggs
            for (int i = 0; i < YellowEggs.Count; i++)
            {
                if (YellowEggSlots.Count >= i && YellowEggs.Count >= i)
                {
                    if (YellowEggSlots[i] != null && YellowEggs[i] != null)
                    {
                        YellowEggs[i].transform.position = YellowEggSlots[i].position;
                    }
                }
            }
        }



        //Food Dose't Break

        //for Updating The postion of Food
        for (int i = 0; i < Food.Count; i++)
        {
            if (FoodSlots.Count >= i && Food.Count >= i)
            {
                if (FoodSlots[i] != null && Food[i] != null)
                {
                    Food[i].transform.position = FoodSlots[i].position;
                }
            }
        }
        
    }


}
/*هذا الكلاس هو المسؤل عن
1- يقوم بنقل الكائنات التي يمكن تخزينها إلى مخزنها الصحيح
2- يقوم برمي الكائنات التي يمكن تخزينها بعيدا إذا كان المخزن ممتلئ
3- يقوم برمي الكائنات الموجودة في المخزن عند الضغط على الزر الأيسر
4- يمكنك ذيادة سعة كل انبوب من الإنسبكتر
    */
