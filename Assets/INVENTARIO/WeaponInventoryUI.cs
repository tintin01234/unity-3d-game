using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryUI : MonoBehaviour
{
    public Image[] slots;

    public void ActualizarUI(InventoryItem[] weapons, int armaEquipada)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Apagar todos los slots
            slots[i].enabled = false;
        }

        // Mostrar SOLO el arma equipada
        if (weapons[armaEquipada] != null)
        {
            slots[armaEquipada].sprite = weapons[armaEquipada].icon;
            slots[armaEquipada].enabled = true;
        }
    }
}
