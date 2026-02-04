using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;
    public InventoryManager inventoryManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("INTENTANDO RECOGER ARMA: " + gameObject.name);
            RecogerArma();
        }
    }

    void RecogerArma()
    {
        if (weaponPrefab == null)
        {
            Debug.LogError("Prefab de arma NULL en: " + gameObject.name);
            return;
        }

        inventoryManager.EquiparArma(weaponPrefab);
        Destroy(gameObject);
    }
}

