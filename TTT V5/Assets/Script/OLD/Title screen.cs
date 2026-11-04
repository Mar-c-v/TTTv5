using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
//moverse entre escenas
    public void EscenaInicial()
    {
        SceneManager.LoadScene("Main menu");
    }

  
//Cerrar el programa
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
