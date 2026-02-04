using UnityEngine;

public class TestTeclaI : MonoBehaviour
{
    public GameObject panel;

    private bool activo = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activo = !activo;
            panel.SetActive(activo);
            Debug.Log("Inventario: " + (activo ? "Abierto" : "Cerrado"));
        }
    }
}