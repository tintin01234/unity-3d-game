using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*administra las monedas del jugador, genera monedas cuando la mina está activa, metodo recibir dinero que le da una cantidad especifica de dinero, se
muestra todo el dinero total por pantalla
*/
public class economiaJugador : MonoBehaviour
{
    public int cantidadMonedas;
    public TextMeshProUGUI cantMonedas; // Asigna el texto de UI que muestra las monedas
    public float tiempoJuego = 0f; // Controla el tiempo para generar monedas automaticamente

    void Start()
    {
        cantidadMonedas = 0;
        ActualizarUI();
    }

    void Update()
    {
        // Genera automaticamente monedas si la mina esta activa
        if (cantidadMonedas > 0)
        {
            tiempoJuego += Time.deltaTime; //guardo el tiempo en segundos en tiempoJuego
            if (tiempoJuego >= 1f)  // Genera una moneda cada segundo si se activa la mina
            {
                cantidadMonedas += 1;
                tiempoJuego = 0f;
                ActualizarUI();
            }
        }
    }

    public void RecibirMonedas(int cantidadRecibida)
    {
        cantidadMonedas += cantidadRecibida;
        Debug.Log("Monedas actuales: " + cantidadMonedas);
        ActualizarUI();
        //aumenta la cantidad de monedas del jugador, la variable y la muestra por pantalla en el Hud
    }

    private void ActualizarUI()
    {
        // Actualiza el texto de la UI con la cantidad actual de monedas
        if (cantMonedas != null)
        {
            cantMonedas.SetText(cantidadMonedas.ToString());
        }
        else
        {
            Debug.LogWarning("El campo 'cantMonedas' no esta asignado en el Inspector.");
        }
    }
}
