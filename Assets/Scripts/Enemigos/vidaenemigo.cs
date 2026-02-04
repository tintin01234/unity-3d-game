using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
Escena main: marmonanerf2 (enemigo comun) y marmotacetro3 (enemigo jefe) 
Desde caracteristicasArmaOriginal se aplica la funcion TakeDamage al enemigo que reciba un disparo
Los enemigos tienen una barra de vida que esta activa solo si recibieron daño recientemente
Al morir da un valor de dinero entre un numero minimo y uno maximo al jugador
*/
public class VidaEnemigo : MonoBehaviour
{
    //
    public GameObject vidamarmota;
    int vidaactual;
    public int vidamax = 10;
    //
    int valorMonedas;
    public int valorMinMonedas;
    public int valorMaxMonedas;
    //Se asigna al jugador para que este reciba dinero al morir la marmota
    public GameObject player;
    //Se asigna los sliders de vida de la marmota y cada cuanto tiempo esta se desactiva al no recibir daño
    public Slider barraDeVida;
    public Slider barraDeVidaAmarilla;
    private float velBarraAmarilla = 0.05f;
    public float tiempovida = 0.0f;

    //
    void Start()
    {
        vidamarmota.SetActive(false);
        vidaactual = vidamax;
        barraDeVida.maxValue = vidamax;
        barraDeVida.value = vidaactual;
        barraDeVidaAmarilla.value = vidaactual;
        barraDeVidaAmarilla.maxValue = vidamax;
    }

    //Al recibir un disparo del arma esta si entra en colision con un objeto con el tag enemigo llama esta funcion
    public void TakeDamage(int damage)
    {
        ReducirVida(damage);
        if (vidaactual <= 0)
        {
            Die();
        }
    }

    //Activa el slider que muestra la vida actual de la marmota y reduce el daño que recibio
    public void ReducirVida(int cantidad)
    {
        vidamarmota.SetActive(true);
        vidaactual -= cantidad;
        vidaactual = Mathf.Clamp(vidaactual, 0, vidamax);
        barraDeVida.value = vidaactual;
        tiempovida = 0f;
    }

    //
    public bool IsDead()
    {
        return vidaactual <= 0;
    }

    //Verifica que haya pasado menos de cierta cantidad de tiempo al recibir daño para seguir mostrando la barra de vida
    void Update()
    {
        tiempovida += Time.deltaTime;
        if (barraDeVida.value != barraDeVidaAmarilla.value)
        {
            barraDeVidaAmarilla.value = Mathf.Lerp(barraDeVidaAmarilla.value, vidaactual, velBarraAmarilla);
            if (tiempovida >= 2f)
            {
                vidamarmota.SetActive(false);
                tiempovida = 0f;
            }
            
        }
      
    }

    //Que pasa si el enemigo muere (vidaactual <= 0), el jugador recibe un valor de dinero entre un valor minimo y maximo y se destruye la marmota
    private void Die()
    {
        valorMonedas = UnityEngine.Random.Range(valorMinMonedas, valorMaxMonedas + 1);
        player.GetComponent<economiaJugador>().RecibirMonedas(valorMonedas);
        Destroy(gameObject);
    }
}