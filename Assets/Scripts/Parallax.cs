using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase para hacer efecto parallax con un fondo
/// Este script se lo tenemos que poner como componente al background con el que queremos hacer el efecto
/// </summary>
public class Parallax : MonoBehaviour
{
    Transform cam;//hace referencia al componente Transform de la camara
    Vector3 previousCamPos; //guardo la posición de la camara en el frame anterior

    public float distance;//valor que vamos a usar para determinar "como de lejos está el fondo del player"
    public float smoothing = 1;//"velocidad" del efecto parallax

    void Awake()
    {
        cam = Camera.main.transform;//Camera.main hace referencia a la camara de la escena que tiene la etiqueta de
                                    //"MainCamera"
        previousCamPos = cam.position;
    }
    void Update()
    {
        //variable float donde almacenamos la distancia que se ha movida la camara en el eje X y lo multiplicamos
        //por el valor de distance que es el que me dice si el background está muy lejos o no
        float parallax = (previousCamPos.x - cam.position.x) * distance;
        //posición a la que quiero mover el background(cambiar su coordenada X)
        Vector3 backgroundPos = new Vector3(transform.position.x + parallax, transform.position.y, transform.position.z);

        //muevo el background de una forma guay con el lerp
        transform.position = Vector3.Lerp(transform.position, backgroundPos, smoothing * Time.deltaTime);

        //guardamos la posición de la cámara antes de salirnos del update
        previousCamPos = cam.position;
    }
}
