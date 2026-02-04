using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Escena main: Objeto player
Asigna la vida y barra de vida del jugador
Maneja la perdida de vida al recibir daño en intervalos de tiempo con la corrutina ReducirVidaConIntervalo
*/

public class BarraDeVida : MonoBehaviour
{
    public Slider barraDeVida;
    public Slider barraDeVidaAmarilla;
    public float vidaMaxima = 100f;
    public float vidaActual;
    public GameObject pantallaDerrota;
    private Coroutine damageCoroutine;
    private float velBarraAmarilla = 0.05f;

    void OnTriggerStay(Collider other)
    {
       
        GameObject objeto = other.gameObject;
        if ((objeto.tag == "Enemy") && (Time.timeScale == 1))
        {
            EnemyAIMovement enemigo = objeto.GetComponent<EnemyAIMovement>();
            if (enemigo != null && damageCoroutine == null)
            {
                // Empieza a hacer daño a intervalos de tiempo si se encuentra con un ememigo y el juego esta corriendo, es decir, el tiempo esta en 1
                damageCoroutine = StartCoroutine(ReducirVidaConIntervalo(enemigo));
               
            }
        }
        else
        {

        }
    }
    void OnTriggerExit(Collider other)
    {
        // Detiene el daño cuando el enemigo sale del área
        if (other.CompareTag("Enemy") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator ReducirVidaConIntervalo(EnemyAIMovement enemigo)
    {
        while (true)
        {
            ReducirVida(enemigo.Damage); // Aplica el daño del enemigo
            yield return new WaitForSeconds(enemigo.attackCooldown); // Espera el tiempo de cooldown del ataque del enemigo (para que no haya oneshot)
        }
    }
    void Start()
    {
        vidaActual = vidaMaxima;
        barraDeVida.maxValue = vidaMaxima;
        barraDeVida.value = vidaActual;
        barraDeVidaAmarilla.value = vidaActual;
        barraDeVidaAmarilla.maxValue = vidaMaxima;
        //se asignan los valores de vida
    }

    void Update()
    {
        if (vidaActual <= 0)
        {
            Time.timeScale = 0;
            pantallaDerrota.SetActive(true); //si la vida es 0 o menor, se activa el canva de pantallaDerrota
        }
        if (barraDeVida.value != barraDeVidaAmarilla.value)
        {
            barraDeVidaAmarilla.value = Mathf.Lerp(barraDeVidaAmarilla.value, vidaActual, velBarraAmarilla);
        } //si se reduce la vida del jugador, muestra una barra de vida amarilla que también bajará pero mas lento
    }

    //reduce la vida actual y sincroniza la barra de vida con vidaActual
    public void ReducirVida(float cantidad) 
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        barraDeVida.value = vidaActual;
    }
}
