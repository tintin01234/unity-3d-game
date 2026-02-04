using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeArma : MonoBehaviour
{
    public GameObject[] armas; // Array de armas que estarán en la cámara del jugador
    private int indiceArmaActiva = 0; // Índice del arma que está activa

    void Start()
    {
        // Desactivar todas las armas al inicio excepto la primera
        for (int i = 0; i < armas.Length; i++)
        {
            if (armas[i] != null)
            {
                armas[i].SetActive(i == indiceArmaActiva);
            }
        }
    }

    void Update()
    {
        // Cambiar de arma con las teclas 1, 2 y 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CambiarArma(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CambiarArma(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CambiarArma(2);
        }
    }

    void CambiarArma(int indice)
    {
        if (indice >= 0 && indice < armas.Length && indice != indiceArmaActiva)
        {
            // Desactivar el arma actual
            armas[indiceArmaActiva].SetActive(false);

            // Activar la nueva arma
            armas[indice].SetActive(true);
            indiceArmaActiva = indice;
        }
    }
}
