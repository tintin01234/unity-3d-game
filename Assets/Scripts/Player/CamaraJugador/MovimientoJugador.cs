using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Escena main: Player
Gira en torno al uso del rigidbody del jugador
Orientacion del jugador
Control de velocidad del jugador
Control de salto del jugador
*/

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velMovimiento;
    public float dragSuelo;
    public float fuerzaSalto;
    public float cooldownSalto;
    public float aireMultiplier;
    bool listoParaSaltar;

    [Header("Control Movimiento")]
    public float alturaJugador;
    public LayerMask suelo;
    bool enSuelo;

    [Header("HotKeys")]
    public KeyCode teclaSalto = KeyCode.Space;

    public Transform Orientacion;

    float inputHorizontal;
    float inputVertical;

    Vector3 direccionMovimiento;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        listoParaSaltar = true;
    }

    
    void Update()
    {

        enSuelo = Physics.Raycast(transform.position, Vector3.down, alturaJugador * 0.5f + 0.2f, suelo);
        MiInput();

        LimitarVelocidad();

        if (enSuelo)
        {
            rb.drag = dragSuelo;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MoverPlayer();
    }

    private void MiInput()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(teclaSalto) && listoParaSaltar && enSuelo)
        {
            listoParaSaltar = false;

            Jump();

            Invoke(nameof(ResetearSalto), cooldownSalto);
        }
    }

    private void MoverPlayer()
    {
        direccionMovimiento = Orientacion.forward * inputVertical + Orientacion.right * inputHorizontal;

        if (enSuelo) 
        { 
        rb.AddForce(direccionMovimiento.normalized * velMovimiento * 10f, ForceMode.Force);
        }
        else if (!enSuelo)
        {
            rb.AddForce(direccionMovimiento.normalized * velMovimiento * 10f * aireMultiplier, ForceMode.Force);
        }
    }

    private void LimitarVelocidad()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > velMovimiento)
        {
            Vector3 velLimitada = flatVel.normalized * velMovimiento;
            rb.velocity = new Vector3(velLimitada.x, rb.velocity.y, velLimitada.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * fuerzaSalto, ForceMode.Impulse);
    }
    private void ResetearSalto()
    {
        listoParaSaltar = true;
    }
}
