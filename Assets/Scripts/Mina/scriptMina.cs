using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

/*
Escena main: punto compra 1 el objeto que tiene el script
Cuando el jugador esta parado dentro del trigger del objeto con este script, activa el canva para comprar el taladro
Si apretas la tecla E para comprar y tenes el dinero te lo descuenta al coste, si no, no pasa nada
Al salir del trigger se desactiva el canva diciendo el coste

Se itera monedas cada x tiempo en una zona general alrededor del objeto lugarSpawnMonedas
*/

public class scriptMina : MonoBehaviour
{
    public GameObject mina;
    public GameObject hudMina;
    public GameObject player;
    public GameObject monedaPrefab;
    public int coste;
    public float intervaloGeneracionMonedas = 5f; // Intervalo en segundos para generar monedas
    private bool construido;
    private float tiempoGeneracion;
    economiaJugador monedas;
    int dinero;
    public TextMeshProUGUI costeTexto;
    public GameObject lugarSpawnMonedas;
    int cantMonedasPorCiclo;
    public int cantMonedasPorCicloInicial = 5;

    void Start()
    {
        cantMonedasPorCiclo = cantMonedasPorCicloInicial;
        construido = false;
        hudMina.SetActive(false);
        mina.SetActive(false);
        monedas = player.GetComponent<economiaJugador>(); //extraigo del script economiaJugador todos los valores y componentes
        costeTexto.SetText("Coste: " + coste); //muestro por pantalla el coste
        tiempoGeneracion = 0f;  
        //desactivo todo y pongo tiempoGeneración en 0
    }

    void Update()
    {
        dinero = monedas.cantidadMonedas; //guardo la función dentro de una variable

        // Genera monedas en intervalos despues de construir la mina, dirigiendose a la funcion GenerarMoneda
        if (construido)
        {
            tiempoGeneracion += Time.deltaTime; //Guardo en tiempoGeneracion el tiempo en segundos
            if (tiempoGeneracion >= intervaloGeneracionMonedas)
            {
                GenerarMoneda();
                tiempoGeneracion = 0f; // Reinicia el temporizador
            }
        }
    }

    void OnTriggerStay(Collider objeto)
    {
        if (objeto.CompareTag("Player"))
        {
            hudMina.SetActive(true);  

            if (Input.GetKey(KeyCode.E) && dinero >= coste && !construido)
            {
                mina.SetActive(true);
                construido = true;
                monedas.RecibirMonedas(-coste);
                cantMonedasPorCiclo = cantMonedasPorCicloInicial;
            }
        }
        //si el tag que esta dentro del trigger del objeto es Player, Activo el Hud, y si dentro de este, se presiona e,
        // el dinero es suficiente (osea >=), y la mina no se construyo, contruyo la mina, resto la plata y cambio el booleano a true
    } 

    private void OnTriggerExit(Collider objeto)
    {
        hudMina.SetActive(false); //desactivo el HudMina al salir del trigger
    }


    public void GenerarMoneda()
    {
        for(int i = 0; i < cantMonedasPorCiclo; i++) 
        {
            Vector3 posicionmoneda = lugarSpawnMonedas.transform.position + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
            GameObject moneda = Instantiate(monedaPrefab, posicionmoneda, Quaternion.identity);
            moneda.SetActive(true);
        }
        cantMonedasPorCiclo--;
        VerificarMina();
        //genero una moneda en una posición random dentro de los parametros marcados
    }

    public void VerificarMina()
    {
        if (cantMonedasPorCiclo == 0)
        {
            construido = false;
            mina.SetActive(false);
            Debug.Log("Se acabaron las monedas");
        }
    }
}