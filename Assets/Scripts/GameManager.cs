using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int FoodAmount;

    //Tools Icon Hide And Show
    public GameObject[] VacumeParts;
    public GameObject[] WeaponParts;
    void HideVacuum()
    {
        foreach (var item in VacumeParts)
        {
            item.SetActive(false);
        }
    }
    void ShowVacuum()
    {
        foreach (var item in VacumeParts)
        {
            item.SetActive(true);
        }
    }
    void HideWeapon()
    {
        foreach (var item in WeaponParts)
        {
            item.SetActive(false);
        }
    }
    void ShowWeapon()
    {
        foreach (var item in WeaponParts)
        {
            item.SetActive(true);
        }
    }
    public void HideAndeShowIcons()
    {
        if (PlayerManager.HasVacuum)
            ShowVacuum();
        else
            HideVacuum();

        if (PlayerManager.HasWeapons)
            ShowWeapon();
        else
            HideWeapon();

    }

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


    bool MapOpen;
    public GameObject MapCanvas;
    void MapHideAndShow()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            MapOpen = !MapOpen;
            MapCanvas.SetActive(MapOpen);
        }
    }


    void Start()
    {
        CorentTool = Tools.Nothing;
        HighLightTheSellected();
        HideAndeShowIcons();
    }

    float Delay = 1;
    float delay1;
    void Update()
    {
        MapHideAndShow();
        ToolSelection();    //This Function Work As An Item System
        //to avoid Hideing And unhiding Every frame
        delay1 -= Time.deltaTime;
        if(delay1 <= 0)
        {
            HideAndeShowIcons();
            HighLightTheSellected();
            delay1 = Delay;
        }
    }
}
