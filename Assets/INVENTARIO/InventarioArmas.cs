using UnityEngine;
using UnityEngine.UI;

public class InventarioArmas : MonoBehaviour
{
    public GameObject inventoryUI; // El contenedor de las imágenes
    public GameObject[] imagenesArmas; // Las imágenes UI de las 3 armas
    private int armaSeleccionada = 0;
    private bool inventarioActivo = false;

    void Start()
    {
        SeleccionarArma(0);
        inventoryUI.SetActive(false); // Empieza oculto
    }

    void Update()
    {
        // Mostrar/Ocultar inventario con i
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventarioActivo = !inventarioActivo;
            inventoryUI.SetActive(inventarioActivo);
        }

        // Cambiar arma solo si el inventario está activo (opcional)
        if (inventarioActivo)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SeleccionarArma(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SeleccionarArma(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SeleccionarArma(2);
        }
    }

    void SeleccionarArma(int indice)
    {
        for (int i = 0; i < imagenesArmas.Length; i++)
        {
            imagenesArmas[i].SetActive(i == indice);
        }

        armaSeleccionada = indice;
        Debug.Log("Seleccionaste arma: " + armaSeleccionada);
    }
}

