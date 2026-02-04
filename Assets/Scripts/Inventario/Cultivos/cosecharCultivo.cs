using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// slider al cosechar los cultivos ya crecidos, desactiva el movimiento del jugador (script) al cosecharlos, muestra un slider que va disminuyendo cuando se mantiene presionada la E hasta que desaparece cuando los cultivos son cosechados 
public class CosecharCultivo : MonoBehaviour
{
    //Variables que toman los GameObjects para administrar el tiempo de cosecha y que se cosecha
    public GameObject[] cultivosListosParaCosechar; 
    public GameObject canvaCosecha;
    public GameObject barraCosecha;
    public Slider sliderCosecha;
    public GameObject player;

    //Variables que toman cuanto tiempo hay que presionar la E y verifica que estemos parados en la zona de cosecha
    public float tiempoBasePresionE = 3.0f; 
    private float tiempoCosecha;
    private bool triggerStayBool;

    //Se asigna la barra de cosecha como totalmente llena y se desactiva el canva, barra y bool de cosecha
    void Start()
    {
        barraCosecha.SetActive(false);
        triggerStayBool = false;
        canvaCosecha.SetActive(false);
        sliderCosecha.value = 1f;
    }

    //Se crea un int que se le asigna el valor que devuelve contarcultivosactivos constantemente
    //Se toma varias condiciones para empezar a vaciar la barra de cosecha si todas se cumplen
    //Cuando se vacia la barra llama a cosecharcultivos y reseteartiempocosecha
    void Update()
    {
        int cultivosActivos = ContarCultivosActivos();

        if (triggerStayBool && cultivosActivos > 0 && Input.GetKey(KeyCode.E))
        {
            var script = player.GetComponent<MovimientoJugador>();
            barraCosecha.SetActive(true);
            float tiempoRequerido = tiempoBasePresionE * cultivosActivos;
            tiempoCosecha += Time.deltaTime;
            sliderCosecha.value = 1f - (tiempoCosecha / tiempoRequerido);
            script.enabled = false;

            if (tiempoCosecha >= tiempoRequerido)
            {
                CosecharCultivos();
                ResetearTiempoCosecha();
            }
        }
        else
        {
            var script = player.GetComponent<MovimientoJugador>();
            script.enabled = true;
            barraCosecha.SetActive(false);
            ResetearTiempoCosecha();
        }
    }

    //Cuenta de una lista definida en el editor cuantos gameobject cultivos estan activos
    private int ContarCultivosActivos()
    {
        int activos = 0;
        foreach (var cultivo in cultivosListosParaCosechar)
        {
            if (cultivo.activeSelf)
                activos++;
        }
        return activos;
    }

    //Agarra de la lista definida en el editor todos los cultivos listos para cosechar y los cosecha
    private void CosecharCultivos()
    {
        foreach (var cultivo in cultivosListosParaCosechar)
        {
            if (cultivo.activeSelf)
            {
                var cultivoScript = cultivo.GetComponent<RecogerCultivo>();
                cultivoScript.DarDinero();
                cultivo.SetActive(false);
            }
        }
    }

    //Mientras el player este en el trigger de la parcela para cosechar y haya al menos 1 cultivo para cosechar se activa el canva que muestra la barra de cosecha
    void OnTriggerStay(Collider objeto)
    {
        if (objeto.tag == ("Player"))
        {
            int cultivosActivos = ContarCultivosActivos();
            canvaCosecha.SetActive(cultivosActivos > 0);
            triggerStayBool = true;
        }
    }

    //Cuando el player deja el trigger se desactiva el canva y barra de cosecha
    void OnTriggerExit(Collider objeto)
    {
        if (objeto.tag == ("Player"))
        {
            barraCosecha.SetActive(false);
            canvaCosecha.SetActive(false);
            triggerStayBool = false;
        }
    }

    //Se resetea el valor del slider cosecha y el contador de tiempo para cosechar
    public void ResetearTiempoCosecha()
    {
        tiempoCosecha = 0f;
        sliderCosecha.value = 1f;
    }
}
