using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform weaponHolder;

    private GameObject armaInstanciada;

    public void EquiparArma(GameObject prefabArma)
    {
        if (prefabArma == null)
        {
            Debug.LogError("Prefab de arma NULL");
            return;
        }

        if (weaponHolder == null)
        {
            Debug.LogError("WeaponHolder NULL");
            return;
        }

        if (armaInstanciada != null)
            Destroy(armaInstanciada);

        armaInstanciada = Instantiate(
            prefabArma,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );

        Debug.Log("ARMA EQUIPADA: " + armaInstanciada.name);
    }
}
