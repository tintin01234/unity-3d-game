using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Escena main: objeto CanvasHUD
Activa al inicio la interfaz que aparece constantemente durante toda la partida
(vida, enemigos restantes, municion, dinero, etc)
*/

public class ActivacionHUD : MonoBehaviour
{
    public GameObject HudObjeto;
    
    void Start()
    {
        HudObjeto.SetActive(true); //activa el objeto Hud para mostrarle al player
    }
}
