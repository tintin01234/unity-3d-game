using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Escena: Asignado a ambos canvasganaste(GanasteHUD) y canvasperdiste(PerdisteHUD)
Los objetos con este script se activan al cumplirse la condicion de victoria o la condicion de derrota, al apretar escape se vuelve al menu principal
 */
public class pantallaFinalizacion : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu Principal"); //si se presiona escape, se dirige la escena a "Menu Principal"
        }
    }
}
