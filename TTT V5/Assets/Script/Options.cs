using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider volume_slider;
    public float volumesliderValue;

    //brightness
    public Slider slider;

    public Image panelBrillo;

    public  GameObject Audio_config_empty;
    public bool Audio_dropdown;

    public bool Video_dropdown;
    public GameObject Video_config_empty;

    [SerializeField] private Dropdown resolutionDropdown;
    private int selectedResolution;

    public Button fullScreenButton;
    public bool fullscreenBool;



    void Start()
    {
        //PlayerPrefs.SetFloat("volumenAudio", 0.5f);
        //PlayerPrefs.SetFloat("brillo", 0.5f);
        //PlayerPrefs.SetInt("SelectResolution", 2);



        volume_slider.value = PlayerPrefs.GetFloat("volumenAudio");
        AudioListener.volume = volume_slider.value;
        //RevisarSiEstoyMute();  

        slider.value = PlayerPrefs.GetFloat("brillo");
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value );


        fullScreenButton.onClick.AddListener(ToggleFullScreen);
        if(PlayerPrefs.GetInt("Fullscreen") == 0)
        {
        fullscreenBool = true;
        }
        else if(PlayerPrefs.GetInt("Fullscreen") == 1)
        {
        fullscreenBool = false;
        }
        

        resolutionDropdown.value = PlayerPrefs.GetInt("SelectResolution");

        Audio_dropdown = false;
        Audio_config_empty.SetActive(false);

        Video_dropdown = false;
        Video_config_empty.SetActive(false);
    }
  
    public void Audio()
    {
         Audio_config_empty.SetActive(true);
         Audio_dropdown = true;
         Video_dropdown = false;
        Video_config_empty.SetActive(false);
    }
    public void Video()
    {
        Audio_config_empty.SetActive(false);
        Audio_dropdown = false;
        Video_dropdown = true;
        Video_config_empty.SetActive(true);
    }

    public void ChangeVolumeSlider(float Volumen)
    {
        volumesliderValue = Volumen;
        AudioListener.volume = volume_slider.value;
        PlayerPrefs.SetFloat("volumenAudio", volume_slider.value);
        //RevisarSiEstoyMute();
    }

    /*
    //SIRVE PARA CAMBIAR LA IMAGEN A SILENCIADO
     void RevisarSiEstoyMute()
    {
    }
    */
    public void ChangeSlider(float valor)
    {
        valor = slider.value;
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
        PlayerPrefs.SetFloat("brillo", valor);
        Debug.Log(PlayerPrefs.GetFloat("brillo"));
    }

    public void DropDownSelection(Dropdown index)
    {

        selectedResolution = index.value;
        if (selectedResolution == 0)
        {
            Screen.SetResolution(1280, 720, fullscreenBool);
        }
        else if (selectedResolution == 1)
        {
            Screen.SetResolution(1366, 768, fullscreenBool);
        }
        else if (selectedResolution == 2)
        {
            Screen.SetResolution(1920, 1080, fullscreenBool);
        }
        PlayerPrefs.SetInt("SelectResolution", selectedResolution);
        Debug.Log(Screen.fullScreen);
    }


     public void ToggleFullScreen()
    {
        fullscreenBool = !fullscreenBool;
        if(fullscreenBool)
        {
        PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else if(!fullscreenBool)
        {
        PlayerPrefs.SetInt("Fullscreen", 1);
        }
        Screen.SetResolution(Screen.width, Screen.height, fullscreenBool);
        Debug.Log("Cambiado modo de ventana");
        Debug.Log(Screen.fullScreen);
    }
    

    public void Reset()
    {
        if(Audio_dropdown)
        {
            volume_slider.value = 0.5f;
            PlayerPrefs.SetFloat("volumenAudio", 0.5f);
            AudioListener.volume = volume_slider.value;
        }
        else if(Video_dropdown)
        {
            slider.value = 0.5f;
            PlayerPrefs.SetFloat("brillo", 0.5f);
            panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value );

            //togglefullscreen y resolucion
            fullscreenBool = true ;
            PlayerPrefs.SetInt("Fullscreen", 0);


            selectedResolution = 2;
            PlayerPrefs.SetInt("SelectResolution", selectedResolution);
            resolutionDropdown.value = selectedResolution;
        }
    }
}
