using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionFour : MonoBehaviour
{

    public Image BlueReactorFill;
    public Image YellowReactorFill;

    public GameObject NextTutorial;

    public static float MissionFourBlueReactor;
    public static float MissionFourYellowReactor;

    void MessionFour()
    {
        BlueReactorFill.fillAmount = MissionFourBlueReactor;
        YellowReactorFill.fillAmount = MissionFourYellowReactor;
    }

    bool HasWin;
    private void Update()
    {
        MessionFour();
        if (BlueReactorFill.fillAmount >= 1 && YellowReactorFill.fillAmount >= 1)
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
