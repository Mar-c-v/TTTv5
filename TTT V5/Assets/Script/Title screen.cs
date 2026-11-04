using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
 
 
    void Update()
    {
         if(Input.GetKey("escape"))
        {
            //Cerrar el programa con escape
            Debug.Log("hola");
                Application.Quit();
                    Debug.Log("adiós");
        }
    }

    public void OnClickEscenaInicial()
    {
        SceneManager.LoadScene("Main Menu");
    }

   
}

