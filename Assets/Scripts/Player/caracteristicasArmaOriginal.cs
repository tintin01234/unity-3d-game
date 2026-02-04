using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
Escena main: Objeto glockparagus
Script para determinar el funcionamiento y parametros de la glock que tiene el personaje
Se usa raycasthit para obtener la vida a reducir del objeto golpeado si es tag enemy
Click Izquierdo para disparar
R para recargar
*/

public class CaracteristicasArmaOriginal : MonoBehaviour
{
    [Header("Propiedades del Arma")]
    public int daño;
    public float fireRate;
    public float rango;
    public float tiempoRecarga;
    public float tiempoEntreRafaga;
    public int tamañoCargador;
    public int tamañoRafaga;
    public bool mantenerApretado;

    [Header("Estado de Munición")]
    int balasRestantes;
    int balasDisparadas;

    [Header("Control de Disparo")]
    bool disparando;
    bool listoParaDisparar;
    bool recargando;

    [Header("Referencias")]
    public Camera fpsCam;
    public Transform puntoSalida;
    public RaycastHit rayHit;
    public LayerMask tagEnemigo;
    public AudioSource audioSource;
    public AudioClip disparoSonido;
    public AudioClip recargaSonido;
    public TextMeshProUGUI text;
    public int contadorAsesinatos;

    private void MiInput()
    {
        if (mantenerApretado)
        {
            disparando = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            disparando = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if (Input.GetKeyDown(KeyCode.R) && balasRestantes < tamañoCargador && !recargando && (Time.timeScale == 1))
        {
            Recargar();
        }

        if (listoParaDisparar && disparando && !recargando && balasRestantes > 0 && (Time.timeScale == 1))
        {
            balasDisparadas = tamañoRafaga;
            Disparar();
        }
    }
    private void Disparar()
    {
        listoParaDisparar = false;

        audioSource.PlayOneShot(disparoSonido);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, rango, tagEnemigo) && (Time.timeScale == 1))
        {
            Debug.Log(rayHit.collider.name);

            VidaEnemigo vidaEnemigo = rayHit.collider.gameObject.GetComponent<VidaEnemigo>();
            if (vidaEnemigo != null)
            {
                vidaEnemigo.TakeDamage(daño);
            }
        }

        balasRestantes--;
        balasDisparadas--;

        Invoke(nameof(ResetDisparo), fireRate);

        if (balasDisparadas > 0 && balasRestantes > 0 && (Time.timeScale == 1))
        {
            Invoke(nameof(Disparar), tiempoEntreRafaga);
        }
    }
    private void ResetDisparo()
    {
        listoParaDisparar = true;
    }
    private void Recargar()
    {
        recargando = true;

        audioSource.PlayOneShot(recargaSonido);

        Invoke("RecargaTerminada", tiempoRecarga);
    }

    private void RecargaTerminada()
    {
        balasRestantes = tamañoCargador;
        recargando = false;
    }
    void Start()
    {
        contadorAsesinatos = 0;
        balasRestantes = tamañoCargador;
        listoParaDisparar = true;
    }


    void Update()
    {
        MiInput();

        text.SetText(balasRestantes + " / " + tamañoCargador);
    }
}
