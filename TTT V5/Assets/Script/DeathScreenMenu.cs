using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     //Cerrar el programa
    public void Salir()
    {
        SceneManager.LoadScene("Title screen");
    }

    public void Game()    //No se como guardar el juego
    {
        SceneManager.LoadScene("Game");
    }

}
