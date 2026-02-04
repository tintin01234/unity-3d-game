using TMPro;
using UnityEngine;

public class CaracteristicasArma : MonoBehaviour
{
    [Header("Arma Actual")]
    public InventoryItem armaActual;
    private GameObject armaInstanciada;

    [Header("Referencias")]
    public Camera fpsCam;
    public TextMeshProUGUI text;
    public Transform posicionArmaJugador; // WeaponHolder

    [Header("Estado")]
    private bool listoParaDisparar = true;
    private bool recargando = false;
    private int balasActuales;

    private void Update()
    {
        if (armaActual == null) return;

        MiInput();
        ActualizarUI();
    }

    // =========================
    // INPUT
    // =========================
    private void MiInput()
    {
        bool disparando = armaActual.mantenerApretado
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (Input.GetKeyDown(KeyCode.R) &&
            balasActuales < armaActual.tamañoCargador &&
            !recargando)
        {
            Recargar();
        }

        if (listoParaDisparar &&
            disparando &&
            !recargando &&
            balasActuales > 0)
        {
            Disparar();
        }
    }

    // =========================
    // DISPARO
    // =========================
    private void Disparar()
    {
        listoParaDisparar = false;
        balasActuales--;

        armaActual.Disparar(fpsCam);

        Invoke(nameof(ResetDisparo), armaActual.fireRate);
    }

    private void ResetDisparo()
    {
        listoParaDisparar = true;
    }

    // =========================
    // RECARGA
    // =========================
    private void Recargar()
    {
        recargando = true;
        Invoke(nameof(RecargaTerminada), armaActual.tiempoRecarga);
    }

    private void RecargaTerminada()
    {
        balasActuales = armaActual.tamañoCargador;
        recargando = false;
    }

    // =========================
    // EQUIPAR ARMA
    // =========================
    public void Equipar(InventoryItem weapon)
    {
        if (weapon == null) return;

        armaActual = weapon;
        balasActuales = weapon.tamañoCargador;

        if (armaInstanciada != null)
        {
            Destroy(armaInstanciada);
        }

        if (weapon.modeloArma != null && posicionArmaJugador != null)
        {
            armaInstanciada = Instantiate(
                weapon.modeloArma,
                posicionArmaJugador.position,
                posicionArmaJugador.rotation,
                posicionArmaJugador
            );
        }
    }

    // =========================
    // UI
    // =========================
    private void ActualizarUI()
    {
        if (text == null) return;

        text.SetText($"{balasActuales} / {armaActual.tamañoCargador}");
    }
}
