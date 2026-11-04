using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] Tutorial;
    public int i;
    public bool TutorialBool;
    // Start is called before the first frame update
    void Start()
    {
     
     i = 0;
     Tutorial[i].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space key was pressed");
            Tutorial[i].SetActive(false);
            i++;
        }

        if(i<=5)
        {
            TutorialBool = true;
            Tutorial[i].SetActive(true);
        }
        if(i==5)   
        {
         TutorialBool = false;
        }

        if(PlayerPrefs.GetInt("Player_health") <= 0 || MenuManager.Instance.GetComponent<PauseGame>().GameIsPaused == true)
        {
            for(i = 0; i <= 5; i++)
            {
                Tutorial[i].SetActive(false);
            }
        }
    }
}
