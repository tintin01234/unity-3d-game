using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//test
//Genera dinero de forma pasiva
public class generarDineroMina : MonoBehaviour
{
    public int tiempoPorPlata;  // tiempo (en segundos)
    public int cantDineroPorTick;  // Dinero que se genera por segundo
    private float rateGeneracionDinero = 0f;  // Acumula el tiempo en el update
    public GameObject player;

    void Update()
    {
        rateGeneracionDinero += Time.deltaTime;

        // pregunta si rateGeneracionDinero >= tiempoPorPlata (>= ya que nunca va a poder ser igual debido a la velocidad que se guarda la info en el update)
        if (rateGeneracionDinero >= tiempoPorPlata)
        {
            
            player.GetComponent<economiaJugador>().RecibirMonedas(cantDineroPorTick); // utilizo la función RecibirMonedas de economíaJugador y le ingreso como atributo cantDineroPorTick

            
            rateGeneracionDinero = 0f; //reseteo rateGeneracionDinero para que se vuelva a acumular y se vuelva a generar el if
        }
    }
}
