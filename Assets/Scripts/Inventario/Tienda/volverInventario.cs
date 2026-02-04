using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//regresar a inventario (es decir, deshabilitar todos los paneles de la tienda y habilitar el canvas de inventario)
public class volverInventario : MonoBehaviour
{
    //Variables que toman los gameobjects que se tienen que cerrar y abrir cuando apretas volver en cualquier pestaña de la tienda
    public GameObject inventario; //canvas inventario
    public GameObject panelArmas; //panel armas
    public GameObject panelConsumibles; //panel consumibles
    public GameObject panelHabilidades; //panel habilidades

    //Funcion que se llama al tocar el boton volver en cualquier pestaña de la tienda
    public void PasarInventario()
    {
        inventario.SetActive(true);
        panelArmas.SetActive(false);
        panelConsumibles.SetActive(false);
        panelHabilidades.SetActive(false);
    }
}
