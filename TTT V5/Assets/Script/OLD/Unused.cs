using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
        public Texture BoxTexture;      // Drag a Texture onto this item in the Inspector

        GUIContent content;

    // Start is called before the first frame update
    void Start()
    {
         content = new GUIContent("This is a box", BoxTexture, "This is a tooltip");
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKey("escape"))
        {
           //crear el fondo
         
            GUI.Box(new Rect(550, 25, 1345, Screen.height - 50), "");
            
        }
    }
        

        void OnGUI()
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), content);
        }
    
}
