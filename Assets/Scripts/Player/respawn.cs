using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Escena main: objeto Player
El player al tener una posicion menor a y -10 es teletransportado a la posicion de puntorespawn
*/

public class respawn : MonoBehaviour
{
    public Transform puntorespawn;
    
    
    void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = puntorespawn.position; //si el jugador cae del mapa, aparecerá donde se encuentra el Empty dentro del juego, es decir, hara un spawn
        }
    }
}
