using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaOpciones : MonoBehaviour
{
    public ControladorOpciones panelOpciones;
    public GameObject PauseMenu_;


   
    void Start()
    {
        
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControladorOpciones>();
        panelOpciones.pantallaOpciones.SetActive(false);
      
    }
    private void Update()
    {
        if(PauseMenu_ == null)
        {
            PauseMenu_ = GameObject.FindGameObjectWithTag("PauseMenu");
        }
        
    }

    // esto rompe varias escenas
    /*  void Update()
      {
          if (Input.GetKeyDown(KeyCode.Escape))
          {
              MostrarOpciones();
          }
      }
      */

    public void MostrarOpciones()
    {
        
        Debug.Log("Mostra opciones");
        panelOpciones.pantallaOpciones.SetActive(true);
        PauseMenu_.SetActive(false);
        /*
        PauseMenu_.GetComponent<PauseGame>().GameIsPaused = false;
        Debug.Log(PauseMenu_.GetComponent<PauseGame>().GameIsPaused);
        */
    
       
    }

    public void NoMostrarOpciones()
    {
        panelOpciones.pantallaOpciones.SetActive(false);
        PauseMenu_.SetActive(true);
    }
}
