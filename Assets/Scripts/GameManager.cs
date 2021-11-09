using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Tools Section
    public enum Tools
    {
        Nothing,
        Vacuum,
        Weapon,
        BlueEggTube,
        YellowEggTube,
        FoodTube
    }

    public static Tools CorentTool;

    void ToolSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CorentTool = Tools.Weapon;
            Debug.Log(CorentTool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CorentTool = Tools.Vacuum;
            Debug.Log(CorentTool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CorentTool = Tools.BlueEggTube;
            Debug.Log(CorentTool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CorentTool = Tools.YellowEggTube;
            Debug.Log(CorentTool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CorentTool = Tools.FoodTube;
            Debug.Log(CorentTool);
        }

    }




    void Start()
    {
        
    }

    void Update()
    {
        ToolSelection();    //This Function Work As An Item System
    }
}
