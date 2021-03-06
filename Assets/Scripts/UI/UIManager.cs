using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int numKiwis;
    public Text textKiwi;
    
    public void AddKiwi()
    {
        numKiwis++;//sumo 1 a mi contador de kiwis
        textKiwi.text = numKiwis.ToString();//lo muestra por la interfaz
    }
}
