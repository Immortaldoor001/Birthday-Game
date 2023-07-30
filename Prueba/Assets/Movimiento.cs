using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [Header("Movimiento")]
    private float MovHorizontal = 0f;
    [SerializeField] private float VelMov;
    [Range( 0, 0.5f)][SerializeField] private float SuavMov;
    private Vector3 velocidad = Vector3.zero;
    private bool miraDer = true;

    [Header("Salto")]
    [SerializeField] private float FuerzaSalto;
    [SerializeField] private LayerMask EsSuelo;
    [SerializeField] private Vector3 dimCaja;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private bool enSuelo;
    private bool salto = false;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        MovHorizontal=Input.GetAxisRaw("Horizontal") * VelMov;
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimCaja, 0f, EsSuelo);
        Mover(MovHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    private void Mover( float mover, bool salto)
    {
        Vector3 velObjetivo = new Vector3(mover, rb2d.velocity.y);
        rb2d.velocity= Vector3.SmoothDamp(rb2d.velocity, velObjetivo,ref velocidad, SuavMov);
        
        if(mover >0 && !miraDer)
        {
            //Girar
            Girar();
        }
        else if (mover < 0 && miraDer)
        {
            //Girar
            Girar();
        }

        if(enSuelo && salto)
        {
            enSuelo = false;
            rb2d.AddForce(new Vector2(0f, FuerzaSalto));
        }
    }

    private void Girar()
    {
        miraDer = !miraDer;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position,dimCaja);
    }
}
