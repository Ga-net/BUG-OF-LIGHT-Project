using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Damage_Heal_UI : MonoBehaviour
{
    Transform Cam;
    public TMP_Text Damage_Heal_Text;

    private void Start()
    {
        Cam = Camera.main.transform;
    }


    void Hide()
    {
        Damage_Heal_Text.alpha -= Time.deltaTime;
        float y = 0;
        y += Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y + (y*8), transform.localPosition.y);
    }

    public void DamageAmount(float Damage)
    {
        Damage_Heal_Text.text = "-".Insert(1, (Damage * 10).ToString());
    }

    public void HealAmount(float Heal)
    {
        Damage_Heal_Text.text = "+".Insert(1, (Heal * 10).ToString());
    }

    private void Update()
    {
        transform.LookAt(transform.position + Cam.forward);
        Hide();
    }
}
