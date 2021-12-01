using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionOne : MonoBehaviour
{
    public static bool CanTakeTheVacuum;

    public GameObject Apple1, Apple2, Apple3, Apple4, Apple5;

    public GameObject CorectKeySoudn;

    public GameObject NextTutorial;

    public static int MissionOneApplesCount;

    bool A1, A2, A3, A4, A5;
    void MessionOne()
    {
        switch (MissionOneApplesCount)
        {
            case 1:
                if (!A1)
                {
                    A1 = true;
                    Apple1.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 2:
                if(!A2)
                {
                    A2 = true;
                    Apple2.SetActive(true);
                    Apple1.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 3:
                if(!A3)
                {
                    A3 = true;
                    Apple3.SetActive(true);
                    Apple2.SetActive(true);
                    Apple1.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 4:
                if(!A4)
                {
                    Apple4.SetActive(true);
                    Apple3.SetActive(true);
                    Apple2.SetActive(true);
                    Apple1.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                    A4 = true;
                }
                break;
            case 5:
                if(!A5)
                {
                    A5 = true;
                    Apple5.SetActive(true);
                    Apple4.SetActive(true);
                    Apple3.SetActive(true);
                    Apple2.SetActive(true);
                    Apple1.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                    CanTakeTheVacuum = true;
                }
                break;
        }
    }


    bool HasWin;
    private void Update()
    {
        MessionOne();
        if (Apple1.activeSelf && Apple2.activeSelf && Apple3.activeSelf && Apple4.activeSelf && Apple5.activeSelf)
        {
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
