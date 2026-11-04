using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public bool lastGame;
    public bool main;
    public bool newGame;
    public bool extras;
    public bool options;

    // Start is called before the first frame update
    void Start()
    {
    lastGame=false;
    main=true;    
    newGame=false;
    extras=false;
    options=false;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnGUI()
    {
        //fondo del menu
        GUI.Box(new Rect(25, 0, 500, Screen.height), "");
        
        //El menu principal
        if(main){
            
            //si existe un guardado de la ultima partida
            if(lastGame){
                if(GUI.Button(new Rect(25, Screen.height/10, 500, 50), "Resume"))
                {
                
                }
            }

            //empezar juego
            if (GUI.Button(new Rect(25, 2*Screen.height/10, 500, 50), "New Game"))
            {
            newGame=true;
            main=false;
            }

            //extras
            if (GUI.Button(new Rect(25, 3*Screen.height/10, 500, 50), "Extras"))
            {
            extras=true;
            main=false;
            }

            //opciones
            if (GUI.Button(new Rect(25, 4*Screen.height/10, 500, 50), "Options"))
            {
            options=true;
            main=false;
            }

            //salir
            if (GUI.Button(new Rect(25, 5*Screen.height/10, 500, 50), "Quit"))
            {
            Application.Quit();
            }
        }

        //si van en cualquier otro menu que no sea el principal, un boton para ir atras aparecera
        if(!main){
            if (GUI.Button(new Rect(25, Screen.height/10, 500, 50), "Resume"))
            {
            main=true;  
            newGame=false;
            extras=false;
            options=false;
            }
        }
        
        //menu de opciones
        if(options){
            //relacionado con el video
            if (GUI.Button(new Rect(25, 3*Screen.height/10, 500, 50), "Extras"))
            {
                SideScreen();
            }

            //relacionado con el sonido
            if (GUI.Button(new Rect(25, 4*Screen.height/10, 500, 50), "Options"))
            {
            options=true;
            main=false;
            }

            //elegir idioma
            if (GUI.Button(new Rect(25, 5*Screen.height/10, 500, 50), "Quit"))
            {
            Application.Quit();
            }
        }
    }

    public void SideScreen(){
    GUI.Box(new Rect(550, 25, 1345, Screen.height - 50), "");
    }
}

