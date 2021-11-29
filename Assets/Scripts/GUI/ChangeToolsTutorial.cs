using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToolsTutorial : MonoBehaviour
{
    public GameObject MouseWheel,N1,N2,N3,N4,N5;

    public GameObject CorectKeySoudn;

    public GameObject NextTutorial;

    void ChangingToolTuto()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !N1.activeSelf)
        {
            N1.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !N2.activeSelf)
        {
            N2.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !N3.activeSelf)
        {
            N3.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !N4.activeSelf)
        {
            N4.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && !N5.activeSelf)
        {
            N5.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }


        if ((Input.GetAxisRaw("Mouse ScrollWheel") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0) && !MouseWheel.activeSelf)
        {
            MouseWheel.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
    }


    bool HasWin;
    private void Update()
    {
        ChangingToolTuto();
        if (MouseWheel.activeSelf && N1.activeSelf && N2.activeSelf && N3.activeSelf && N4.activeSelf && N5.activeSelf)
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
