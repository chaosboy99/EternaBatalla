using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    public float rangoArea;
    public LayerMask CapaPj;
    bool estarAlerta;
    public Transform Pj;
    public float vel;

    public int maxHealth = 10; // La cantidad máxima de salud del enemigo
    private int currentHealth; // La cantidad actual de salud del enemigo


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Al iniciar, la salud actual es igual a la salud máxima
    }

    // Update is called once per frame
    void Update()
    {
        
        estarAlerta = Physics.CheckSphere(transform.position, rangoArea, CapaPj);

        if(estarAlerta == true)
        {
            transform.LookAt(new Vector3(Pj.position.x, transform.position.y, Pj.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Pj.position.x, transform.position.y, Pj.position.z), vel * Time.deltaTime);
        }


    }
    // Método para restar salud al enemigo
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Resta el daño recibido a la salud actual
        Debug.Log(currentHealth);

        if (currentHealth <= 0) // Verifica si la salud actual es menor o igual a cero
        {
            Die(); // Si es así, llama al método Die() para eliminar el enemigo
        }
    }

    // Método para eliminar el enemigo
    void Die()
    {
        Destroy(gameObject); // Destruye el objeto del enemigo
    }
}
