using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndDashTutorial : MonoBehaviour
{
    //Movement
    public GameObject Shift, Space;
   
    public GameObject CorectKeySoudn;

    public GameObject NextTutorial;

    void JumpTutorial()
    {
       if(Input.GetKeyDown(KeyCode.LeftShift) && ( Input.GetAxis("Horizontal") >0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) && !Shift.activeSelf)
        {
            Shift.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }

        if (Input.GetButtonDown("Jump") && !Space.activeSelf)
        {
            Space.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
    }


    bool HasWin;
    private void Update()
    {
        JumpTutorial();
        if (Shift.activeSelf && Space.activeSelf)
        {
            //if(!HasWin)
            DestroAfterCompleation();
        }
    }


    void DestroAfterCompleation()
    {
        if (!HasWin && !gameObject.GetComponent<AudioSource>().isPlaying)
            gameObject.GetComponent<AudioSource>().Play();
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);
        HasWin = true;
    }


    private void OnDestroy()
    {
        NextTutorial.SetActive(true);
    }
}
