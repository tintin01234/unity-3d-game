using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Escena main: moneda mina
Cuando un objeto con el tag player entra al trigger de la moneda esta da el dinero al componente economiajugador
Reproduce un sonido al recogerse
Se destruye al recogerse
*/

public class Moneda : MonoBehaviour
{
    public int valorMoneda;
    public GameObject jugador;
    public AudioSource sourceSonido; //SonidoAlrecogerMoneda
    public AudioClip ruiditoMoneda;
    economiaJugador monedas; //importo de otro scrpit la economia del player asi al agarrar la moneda se le suma a el

    void Start()
    {
        monedas = jugador.GetComponent<economiaJugador>(); //Importo los valores del scrpit
    }

    private void OnTriggerEnter(Collider other) //Ontriggerenter ya que busco que suceda algo al entrar dentro del rango de la moneda
    {

        if (other.CompareTag("Player"))
        {
            monedas.RecibirMonedas(valorMoneda);  //llamo a la funcion del scrpit con el atributo valorMoneda 
            sourceSonido.PlayOneShot(ruiditoMoneda);
            GetComponent<Collider>().enabled = false; //desactivo el Collider de la moneda
            Destroy(gameObject); //destruyo la moneda
        }
    }
}
