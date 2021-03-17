using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public float minValueSize;//tamaño de la camara al que queremos llegar
    public float factor;
    public GameObject uiGameOver;
    Camera cameraC;
    void Awake()
    {
        cameraC = GetComponent<Camera>();
    }

    void Update()
    {
        //si el tamaño de la camara es mayor que el valor al que quiero que llegue
        if(cameraC.orthographicSize > minValueSize)
        {
            //vamos restando valor al size de la cámara
            cameraC.orthographicSize -= Time.deltaTime * factor;
        }
        //si la camara ha llegado al valor que yo quería (el tamaño) y no está activado el uiGameover
        else if(cameraC.orthographicSize <= minValueSize && uiGameOver.activeSelf == false)
        {
            uiGameOver.SetActive(true);
        }
    }
    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
