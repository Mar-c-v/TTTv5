using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool GameIsPaused;
    public  GameObject PauseMenu;
    public  GameObject Winscreeen;
    public  GameObject Endscreeen;
    public GameObject Nextdaybutton;
    void Start()
    {
        Nextdaybutton.GetComponent<Button>().interactable = true;
        PauseMenu.SetActive(false);
        Winscreeen.SetActive(false);
        Endscreeen.SetActive(false);
            GameIsPaused = false;
    }

    //  el escape pot causar problemes si pots obrir i tancar el menu mentres estas a les opcions
    void Update()
    {
         if(Input.GetKeyDown("escape") && !GameIsPaused && MenuManager.Instance.GetComponent<TutorialManager>().TutorialBool == false)
        {
            GameIsPaused = true;
            Nextdaybutton.GetComponent<Button>().interactable = false;
            PauseMenu.SetActive(true);
        }
        
        if(PlayerPrefs.GetInt("Enemy_health_") <= 0)
        {
            Debug.Log("nmy hp 0");
            GameIsPaused = true;
            Nextdaybutton.GetComponent<Button>().interactable = false;
            Winscreeen.SetActive(true);
        }

        if(PlayerPrefs.GetInt("Player_health") <= 0)
        {
            GameIsPaused = true;
            Nextdaybutton.GetComponent<Button>().interactable = false;
            Endscreeen.SetActive(true);
        }
      
      //solucion temporal a posibles bugs de despausar el juego mientras estas en las opciones
       /* else if(Input.GetKeyDown("escape") && GameIsPaused )
        {
            GameIsPaused = false;
            PauseMenu.SetActive(false);
        }
        */
    }

    public void Back()
    {
        GameIsPaused = false;
        Nextdaybutton.GetComponent<Button>().interactable = true;
        PauseMenu.SetActive(false);
    }

    //Cerrar el programa
      public void Salir()
    {
       SceneManager.LoadScene("Title screen");
    }
}
