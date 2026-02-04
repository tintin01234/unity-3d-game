using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using UnityEngine.UI;
//proceso del crecimiento de los cultivos, junto con un contador por cada slot de la parcela cuando el cultivo va creciendo
public class crecimientoCultivos : MonoBehaviour
{
    //Variables para asignar las imagenes que se muestran en la parte cultivos del inventario
    //Variables para administrar el tiempo de crecimiento de los cultivos
    private float tiempoControlCrecer = 0f;
    public float tiempoCrecer;
    public GameObject cultivoCrecido; //gameobject del cultivo al crecer
    private int contadorCultivo;
    public GameObject imgCrecido;
    public Image imagenContador;
    public GameObject imagencontador; 

    //Se asigna valores como si el cultivo no estuviera crecido apenas se activa el GameObject
    void Start()
    {
        imgCrecido.SetActive(false);
        cultivoCrecido.SetActive(false);
        contadorCultivo = 0;
        imagenContador.fillAmount = 1f;
    }

    //Controla el paso del tiempo para el crecimiento del cultivo y cuando alcanza el tiempo asignado desde el editor llama a CrecimientoCultivo
    void Update()
    {
        imagencontador.SetActive(true);
        tiempoControlCrecer += Time.deltaTime;
        imagenContador.fillAmount = 1 - (tiempoControlCrecer / tiempoCrecer);
        if (tiempoControlCrecer >= tiempoCrecer)
        {
            CrecimientoCultivo();
        }
    }

    //Activa el GameObject que muestra el cultivo crecido y la imagen que muestra que esta crecido
    //Desactiva el gameobject que tiene el script y la imagen que funcionaba de timer para ver el progreso del crecimiento
    //Resetea el tiempo de control de crecimiento
    private void CrecimientoCultivo()
    {
        tiempoControlCrecer = 0f;
        Debug.Log("CRECIO EL CULTIVO");
        cultivoCrecido.SetActive(true);
        imagencontador.SetActive(false);
        imgCrecido.SetActive(true);
        gameObject.SetActive(false);
    }
}
