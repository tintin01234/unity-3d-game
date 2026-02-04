using TMPro;
using UnityEngine;

public class AmmoHUD : MonoBehaviour
{
    public static AmmoHUD Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ammoText;
    private IWeapon armaActual;

    private void Awake()
    {
        Instance = this;
    }

    public void SetArmaActual(IWeapon arma)
    {
        armaActual = arma;
        ActualizarHUD();
    }

    public void ActualizarHUD()
    {
        if (armaActual == null) return;
        ammoText.SetText($"{armaActual.BalasRestantes} / {armaActual.TamanoCargador}");
    }
}
