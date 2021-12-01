using System.Collections;
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
        

        

    }


    private void OnTriggerStay(Collider other)
    {
        //taking the blue Eggs
        if (other.CompareTag("Blue EGG") && BlueEggs.Count < TubeMax && Input.GetButton("Fire1"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.localScale /= 2;
            other.transform.position = BlueEggSlots[BlueEggs.Count].position;
            Debug.Log("set to False");
            Debug.Log(other.name);
            BlueEggs.Add(other.gameObject);
            other.gameObject.SetActive(false);
            PlayTakingSound();
        }

        //taking the Yellow Eggs
        if (other.CompareTag("Yellow EGG") && YellowEggs.Count < TubeMax && Input.GetButton("Fire1"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.localScale /= 2;
            other.transform.position = YellowEggSlots[YellowEggs.Count].position;
            Debug.Log("set to False");
            Debug.Log(other.name);
            YellowEggs.Add(other.gameObject);
            other.gameObject.SetActive(false);
            PlayTakingSound();
        }



        //taking the Food
        if (other.CompareTag("Food") && Food.Count < TubeMax && Input.GetButton("Fire1"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.localScale /= 2;
            other.transform.position = FoodSlots[Food.Count].position;
            Debug.Log("set to False");
            Debug.Log(other.name);
            Food.Add(other.gameObject);
            other.gameObject.SetActive(false);
            PlayTakingSound();
        }





        //to throw the BLue Egges When the tube Is Full
        if (other.CompareTag("Blue EGG") && BlueEggs.Count > TubeMax && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            //PlayErrorSound();
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
        }


        //to throw the Yellow Egges When the tube Is Full
        if (other.CompareTag("Yellow EGG") && YellowEggs.Count > TubeMax && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            //PlayErrorSound();
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
        }



        //to throw the Food When the tube Is Full
        if (other.CompareTag("Food") && Food.Count > TubeMax && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            //PlayErrorSound();
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
        }



        //to throw the Other Riggids When the player Try To Cach Them
        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().isKinematic == false && Input.GetButton("Fire1"))
        {
            //نادي دالة رمي الأشياء -
            if(other.CompareTag("Frictions"))
            {
                int Redusnois;
                Redusnois = Random.Range(0, 100);
                if (Redusnois < 1)
                    PlayErrorSound();
            }

            if (!other.CompareTag("Frictions"))
                PlayErrorSound();
            ThrowIt(other.transform, other.GetComponent<Rigidbody>());
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
            BlueEggs[BlueEggs.Count - 1].SetActive(true);
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
            YellowEggs[YellowEggs.Count - 1].SetActive(true);
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
            Food[Food.Count - 1].SetActive(true);
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

        //to remove The broken Blue Egges
        if (BlueEggs.Count > 0)
        {
            for (int i = 0; i < BlueEggs.Count; i++)
            {
                if (BlueEggs[i] == null)
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





    //for Mission 2
    bool BlueX;
    bool YellowX;
    private void LateUpdate()
    {

        if(!BlueX)
        {
            MissionTow.MiisonTowBlueEggCollecting = BlueEggs.Count;
            if (BlueEggs.Count >= 5)
                BlueX = true;
        }

        if (!YellowX)
        {
            MissionTow.MiisonTowYellowEggCollecting = YellowEggs.Count;
            if (YellowEggs.Count >= 5)
                YellowX = true;
        }
    }


    // to Hide The Egges in the tubes when the vacuum are not in use
    public void HideAll()
    {

        foreach (var item in BlueEggs)
        {
            if (item != null)
                item.SetActive(false);
        }
        foreach (var item in YellowEggs)
        {
            if (item != null)
                item.SetActive(false);
        }
        foreach (var item in Food)
        {
            if (item != null)
                item.SetActive(false);
        }
    }


    public void UnHideAll()
    {
        //Useless now

        //foreach (var item in BlueEggs)
        //{
        //    if(item != null)
        //    item.SetActive(true);
        //}
        //foreach (var item in YellowEggs)
        //{
        //    if (item != null)
        //        item.SetActive(true);
        //}
        //foreach (var item in Food)
        //{
        //    if (item != null)
        //        item.SetActive(true);
        //}
    }
    
}
/*هذا الكلاس هو المسؤل عن
1- يقوم بنقل الكائنات التي يمكن تخزينها إلى مخزنها الصحيح
2- يقوم برمي الكائنات التي يمكن تخزينها بعيدا إذا كان المخزن ممتلئ
3- يقوم برمي الكائنات الموجودة في المخزن عند الضغط على الزر الأيسر
4- يمكنك ذيادة سعة كل انبوب من الإنسبكتر
    */
