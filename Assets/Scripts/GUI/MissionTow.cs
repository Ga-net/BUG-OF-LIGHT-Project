using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTow : MonoBehaviour
{
    public static bool CanTakeTheWeapon;

    public GameObject B1, B2, B3, B4, B5;
    public GameObject Y1, Y2, Y3, Y4, Y5;

    public GameObject CorectKeySoudn;

    public GameObject NextTutorial;

    public static int MiisonTowBlueEggCollecting;
    public static int MiisonTowYellowEggCollecting;

    bool BB1, BB2, BB3, BB4, BB5;
    bool YY1, YY2, YY3, YY4, YY5;
    void MessionOne()
    {
        switch (MiisonTowBlueEggCollecting)
        {
            case 1:
                if (!BB1)
                {
                    BB1 = true;
                    B1.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 2:
                if (!BB2)
                {
                    BB2 = true;
                    B2.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 3:
                if (!BB3)
                {
                    BB3 = true;
                    B3.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 4:
                if (!BB4)
                {
                    BB4 = true;
                    B4.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 5:
                if (!BB5)
                {
                    BB5 = true;
                    B5.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
        }

        switch (MiisonTowYellowEggCollecting)
        {
            case 1:
                if (!YY1)
                {
                    YY1 = true;
                    Y1.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 2:
                if (!YY2)
                {
                    YY2 = true;
                    Y2.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 3:
                if (!YY3)
                {
                    YY3 = true;
                    Y3.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 4:
                if (!YY4)
                {
                    YY4 = true;
                    Y4.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
            case 5:
                if (!YY5)
                {
                    YY5 = true;
                    Y5.SetActive(true);
                    Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
                }
                break;
        }
    }


    bool HasWin;
    private void Update()
    {
        MessionOne();
        if ((B1.activeSelf && B2.activeSelf && B3.activeSelf && B4.activeSelf && B5.activeSelf) && (Y1.activeSelf && Y2.activeSelf && Y3.activeSelf && Y4.activeSelf && Y5.activeSelf))
        {
            CanTakeTheWeapon = true;
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
