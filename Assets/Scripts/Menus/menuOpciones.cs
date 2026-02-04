using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Escena menu principal: asignado a objeto menuOpciones
Los botones que tienen asignado la funcion cargarJuego (reglas y jugar por ej) cargan la escena que se le asigna como string desde el editor
El boton salir tiene asignada la funcion salir y tiene solo uso en la build .exe
*/

public class menuOpciones : MonoBehaviour
{
    public void cargarJuego(string nombre)
    {
        SceneManager.LoadScene(nombre);
        Time.timeScale = 1;
        //carga la escena del juego y activa el tiempo
    }
    public void salir() { Application.Quit(); Debug.Log("Saliendo del juego"); } //sale del juego al presionar salir
}
