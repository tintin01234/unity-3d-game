using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//cerrar el canva inventario y abrir el canvas tienda
public class invAtienda : MonoBehaviour
{
    //Variables que toman los GameObjects que se tienen que cerrar y abrir
    public GameObject tienda; //canvas tienda
    public GameObject inventario; //canvas inventario

    //Funcion que se llama al apretar tienda desde el inventario
    public void PasarTienda()
    {
        tienda.SetActive(true);
        inventario.SetActive(false);
    }
}
