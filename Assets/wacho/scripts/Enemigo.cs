using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    public float rangoArea; // Rango de detecci�n del jugador
    public float rangoAtaque; // Rango de ataque del jugador
    public LayerMask CapaPj; // Capa del jugador
    bool estarAlerta; // Indica si el enemigo est� alerta o no
    public Transform Pj; // Transform del jugador
    public Animator _anim; // Animator del enemigo
    public float vel; // Velocidad de movimiento del enemigo
    public int vida; // Vida del enemigo

    // Start is called before the first frame update
    void Start()
    {
        // Aqu� puedes inicializar cualquier cosa que necesites al comienzo del juego o la escena
    }

    // Update is called once per frame
    void Update()
    {
        // Comprobar si el jugador est� dentro del rango de detecci�n
        estarAlerta = Physics.CheckSphere(transform.position, rangoArea, CapaPj);

        if (estarAlerta)
        {
            // Calcular la distancia al jugador
            float distancia = Vector3.Distance(transform.position, Pj.position);

            if (distancia <= rangoAtaque)
            {
                // Si el enemigo est� dentro del rango de ataque, ejecutar animaci�n de ataque
                _anim.SetBool("attack", true);
                _anim.SetBool("run", false);
            }
            else
            {
                _anim.SetBool("run", true);
                _anim.SetBool("attack", false);
                // Si el enemigo est� alerta pero fuera del rango de ataque, rotar hacia el jugador y moverse hacia �l
                transform.LookAt(new Vector3(Pj.position.x, transform.position.y, Pj.position.z));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Pj.position.x, transform.position.y, Pj.position.z), vel * Time.deltaTime);
                
            }
        }
        else
        {
            // Si el enemigo no est� alerta, detener animaci�n de ataque y de correr
            _anim.SetBool("attack", false);
            _anim.SetBool("run", false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Comprobar si el enemigo ha colisionado con una bala
        if (other.gameObject.CompareTag("Bullet"))
        {
            vida--; // Reducir la vida del enemigo

            if (vida <= 0)
            {
                Destroy(gameObject); // Si la vida del enemigo llega a cero o menos, destruir el objeto del enemigo
            }
        }
    }
}
