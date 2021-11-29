using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DaysSystem : MonoBehaviour
{
    [Tooltip("how long the day is (the numper in sec")]
    public float DayTime;
    float cornetDay;

    public int SurvivaedDays;

    // GUI
    public Image DayBarTofill;
    
    void NewDayEvent()
    {

    }


    private void Start()
    {
        DayBarTofill = GameObject.Find("Day time fill Fill NAMISIMP").GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        cornetDay += Time.deltaTime;
        DayBarTofill.fillAmount = cornetDay / DayTime;
    }
}
