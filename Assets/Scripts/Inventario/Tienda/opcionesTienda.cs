using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ir entre los interfaces de la tienda (habilidades, armas y consumibles) habilitando uno y deshabilitando los 2 restantes
public class opcionesTienda : MonoBehaviour
{
    //GameObjects de las pestañas(paneles) de la tienda
    public GameObject armas;
    public GameObject consumibles;
    public GameObject habilidades;

    //Activa la pestaña de armas y desactiva las otras 2 aunque ya este una de las 2 desactivada
    public void HabilitarArmas()
    {
        armas.SetActive(true);
        consumibles.SetActive(false);
        habilidades.SetActive(false);
    }

    //Activa la pestaña de Consumibles y desactiva las otras 2 aunque ya este una de las 2 desactivada
    public void HabilitarConsumibles() 
    {
        armas.SetActive(false);
        consumibles.SetActive(true);
        habilidades.SetActive(false);
    }

    //Activa la pestaña de Habilidades y desactiva las otras 2 aunque ya este una de las 2 desactivada
    public void HabilitarHabilidades()
    {
        armas.SetActive(false);
        consumibles.SetActive(false);
        habilidades.SetActive(true);
    }
}
