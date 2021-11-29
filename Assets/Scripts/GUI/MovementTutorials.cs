using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorials : MonoBehaviour
{
    //Movement
    public GameObject W, A, S, D;
    public GameObject Up, Down, Left, Right;

    public GameObject CorectKeySoudn;

    public GameObject NextTutorial;

    void MovementTutorial()
    {
        if (Input.GetKeyDown(KeyCode.W) && !W.activeSelf)
        {
            W.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.A) && !A.activeSelf)
        {
            A.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.S) && !S.activeSelf)
        {
            S.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.D) && !D.activeSelf)
        {
            D.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !Up.activeSelf)
        {
            Up.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !Down.activeSelf)
        {
            Down.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !Left.activeSelf)
        {
            Left.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !Right.activeSelf)
        {
            Right.SetActive(true);
            Instantiate(CorectKeySoudn, transform.position, Quaternion.identity);
        }
    }


    bool HasWin;
    private void Update()
    {
        MovementTutorial();
        if ((W.activeSelf && A.activeSelf && S.activeSelf && D.activeSelf) || (Up.activeSelf && Down.activeSelf && Left.activeSelf && Right.activeSelf))
        {
            //if(!HasWin)
                DestroAfterCompleation();
        }
    }


    void DestroAfterCompleation()
    {
        if(!HasWin && !gameObject.GetComponent<AudioSource>().isPlaying)
            gameObject.GetComponent<AudioSource>().Play();
        if(!gameObject.GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);
        HasWin = true;
    }


    private void OnDestroy()
    {
        NextTutorial.SetActive(true);
    }
}
