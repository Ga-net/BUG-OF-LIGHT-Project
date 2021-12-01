using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DaysSystem : MonoBehaviour
{
    [Tooltip("how long the day is (the numper in sec")]
    public float DayTime;
    float cornetDay;

    public static int SurvivaedDays;

    // GUI
    public TMP_Text SurvivedDaysText;
    public Image DayBarTofill;
    public TMP_Text SurvivedDaysinPlayerstats;
    public Animator Anim;
    public GameObject NewDaySound;

    void NewDayEvent()
    {
        Anim.SetTrigger("NewDay");
            Instantiate(NewDaySound);
    }


    private void Start()
    {
        DayBarTofill = GameObject.Find("Day time fill Fill NAMISIMP").GetComponent<Image>();
        SurvivedDaysinPlayerstats = GameObject.Find("Survived Dayes NAMISIMP").GetComponent<TMP_Text>();
    }


    void DayChanger()
    {
        cornetDay += Time.deltaTime;
        if(cornetDay >= DayTime)
        {
            SurvivaedDays++;
            SurvivedDaysText.text = SurvivaedDays.ToString();
            SurvivedDaysinPlayerstats.text = SurvivaedDays.ToString();
            NewDayEvent();
            cornetDay = 0;
        }
    }



    void Update()
    {
        DayChanger();
        DayBarTofill.fillAmount = cornetDay / DayTime;
    }
}
