using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Tools Section
    public enum Tools
    {
        Weapon,
        BlueEggTube,
        FoodTube,
        YellowEggTube,
        Nothing,
        Vacuum
    }

    public GameObject[] SellectionHighLight;
    public bool HighLightSellectedOrRevers;

    void HighLightTheSellected()
    {
        for (int i = 0; i < SellectionHighLight.Length; i++)
        {
            if ((int)CorentTool == i)
                SellectionHighLight[i].SetActive(HighLightSellectedOrRevers);
            else
                SellectionHighLight[i].SetActive(!HighLightSellectedOrRevers);
        }
    }


    public AudioSource ToolSelectionChange;
    void ToolChangeSound()
    {
        if (!ToolSelectionChange.isPlaying)
            ToolSelectionChange.Play();
    }

    public static Tools CorentTool;

    void ToolSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CorentTool = Tools.Weapon;
            ToolChangeSound();
            HighLightTheSellected();
            Debug.Log(CorentTool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CorentTool = Tools.BlueEggTube;
            ToolChangeSound();
            HighLightTheSellected();
            Debug.Log(CorentTool);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CorentTool = Tools.FoodTube;
            ToolChangeSound();
            HighLightTheSellected();
            Debug.Log(CorentTool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
           
            CorentTool = Tools.YellowEggTube;
            ToolChangeSound();
            HighLightTheSellected();
            Debug.Log(CorentTool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CorentTool = Tools.Nothing;
            ToolChangeSound();
            HighLightTheSellected();
            Debug.Log(CorentTool);
        }


        MouseWheelToolSelection();

        //} else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha0))
        //{

        //}
        //CorentTool = Tools.Vacuum;
        //Debug.Log(CorentTool);
        //Debug.Log(CorentTool);
        //Debug.Log(CorentTool + 0);
    }

    void MouseWheelToolSelection()
    {
        int ToolIndex = 0;
        switch (CorentTool)
        {
            case Tools.Weapon:ToolIndex = 0;
                break;
            case Tools.BlueEggTube:ToolIndex = 1;
                break;
            case Tools.YellowEggTube:ToolIndex = 2;
                break;
            case Tools.FoodTube:ToolIndex = 3;
                break;
            case Tools.Nothing:ToolIndex = 4;
                break;
            case Tools.Vacuum:ToolIndex = 5;
                break;
            default:
                break;
        }


        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (ToolIndex <= 0)
            {
                CorentTool = Tools.Nothing;
                ToolChangeSound();
                HighLightTheSellected();
            }
            else
            {
                CorentTool = CorentTool - 1;
                ToolChangeSound();
                HighLightTheSellected();
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            
            if (ToolIndex >= 4)
            {
                CorentTool = 0;
                ToolChangeSound();
                HighLightTheSellected();
            }
            else
            {
                CorentTool = CorentTool + 1;
                ToolChangeSound();
                HighLightTheSellected();
            }

        }
    }


    void Start()
    {
        HighLightTheSellected();
    }

    void Update()
    {
        ToolSelection();    //This Function Work As An Item System
    }
}
