using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
  /*  void Update()
    {
        if(Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Title screen");
        }
    }
    */

//moverse entre escenas
    public void LoadGame()
    {
        //no se como hacer que continue el juego desde el ultimo guardado aun
        SceneManager.LoadScene("Game");
    }

 
//Cerrar el programa
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
