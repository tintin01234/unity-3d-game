using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class InventoryItem : ScriptableObject
{
    [Header("Datos")]
    public string itemName;
    public Sprite icon;

    [Header("Modelo")]
    public GameObject modeloArma;

    [Header("Disparo")]
    public int daño;
    public float fireRate;
    public float rango;
    public bool mantenerApretado;

    [Header("Munición")]
    public int tamañoCargador;
    public float tiempoRecarga;
    [HideInInspector] public int balasRestantes;

    [Header("Audio")]
    public AudioClip disparoSonido;
    public AudioClip recargaSonido;

    public void Disparar(Camera fpsCam)
    {
        if (balasRestantes <= 0) return;

        balasRestantes--;

        if (Physics.Raycast(
            fpsCam.transform.position,
            fpsCam.transform.forward,
            out RaycastHit hit,
            rango))
        {
            if (hit.collider.TryGetComponent(out VidaEnemigo vida))
            {
                vida.TakeDamage(daño);
            }
        }
    }

    public void Recargar()
    {
        balasRestantes = tamañoCargador;
    }
}

