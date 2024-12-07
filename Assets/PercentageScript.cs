using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PercentageScript : MonoBehaviour
{
    [SerializeField]  Slider ourSlider;

    [SerializeField]  TextMeshProUGUI percentText;


    

    public void SetPercentage(){
        percentText.text = (ourSlider.value * 100).ToString("F0") + "%";
    }
}
