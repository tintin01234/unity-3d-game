using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
Escena main: portales
Al entrar en collision con el objeto si es el player,
este es teletransportado al warp asignado
*/

public class teletransporte : MonoBehaviour
{
    //Asigna el sonido que se reproduce al pasar por el portal
    //Asigna el lugar donde caemos al entrar en contacto con el gameobject
    public Transform warp;
    public AudioSource sonido;

    //Cuando el player entra en colision con el portal teletransporta al objeto que entro en colision al warp asignado en el editor
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject objeto = collision.gameObject;
            objeto.transform.position = warp.position;
            sonido.Play();
        }
    }
}
