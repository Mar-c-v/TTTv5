using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionCode : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
          
        resolutionCode();
       
    }

     public void resolutionCode()
    {
    
        //Patch
        Resolution[] resolutions = Screen.resolutions;
        /* foreach (Resolution res in resolutions)
        {
            print(res.width + "x" + res.height);
        }
        */
        
        //Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);

        //list es una variable preexistente
        List<string> options = new List<string>();

        //tu resolucion va primero
        int currentResolutionIndex = 0;

    
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
    }
    
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution[] resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }
    
}
