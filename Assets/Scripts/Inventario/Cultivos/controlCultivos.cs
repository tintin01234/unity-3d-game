using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Da el oro del cultivo al cosecharlo, conectandose con el scrpit economiaJugador
public class controlCultivos : MonoBehaviour
{
    //Variables que toman el dinero del jugador y el cultivo que se quiere plantar
    public GameObject player;
    economiaJugador dinero;
    int dineroActual;
    public GameObject cultivo; //cultivo dentro del slot
    public int costeCompra;
    
    //Se asigna el cultivo como desactivado y dinero toma los componentes de economiajugador (script que maneja nuestro dinero)
    void Start()
    {
        cultivo.SetActive(false);
        dinero = player.GetComponent<economiaJugador>();
    }

    //Se asigna constantemente el mismo dinero que tiene economiajugador
    void Update()
    {
        dineroActual = dinero.cantidadMonedas;
    }

    //Si el dineroactual es igual o mayor al coste y el cultivo que queremos plantar esta desactivado se planta y resta el costo de nuestra economia
    public void ActivarCultivo()
    {

        if (dineroActual >= costeCompra && (cultivo.activeSelf == false))
        {
            dinero.RecibirMonedas(-costeCompra);
            cultivo.SetActive(true);
            Debug.Log("el cultivo fue plantado");
        }
    }
}
