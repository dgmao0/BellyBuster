using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    
    [SerializeField] bool closedByDefault = true;


void Awake(){
    if(closedByDefault){
        CloseMenu();
    }
}

    public void OpenMenu(){
        GetComponent<Canvas>().enabled = true;
    }

    // Update is called once per frame
    public void CloseMenu(){
        GetComponent<Canvas>().enabled = false;
    }
}
