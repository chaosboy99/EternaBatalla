using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public GameObject bullet;    // Prefab del proyectil
    public Transform spawn;      // Transform de la posici�n de spawn del proyectil

    public float fuerza = 1500;  // Fuerza aplicada al proyectil
    public float shotRate = 0.5f;    // Tiempo m�nimo entre disparos

    private float ShotRateTime = 0;  // Tiempo en el que se puede realizar el siguiente disparo

    // Update se llama una vez por frame
    void Update()
    {
        // Si se presiona el bot�n de disparo (por defecto, el bot�n izquierdo del rat�n)
        if (Input.GetButtonDown("Fire1"))
        {
            // Verificar si ha pasado el tiempo m�nimo entre disparos
            if (Time.time > ShotRateTime)
            {
                // Instanciar un nuevo proyectil en la posici�n y rotaci�n de spawn
                GameObject NewBullet;
                NewBullet = Instantiate(bullet, spawn.position, spawn.rotation);

                // Aplicar una fuerza al proyectil en la direcci�n del spawn
                NewBullet.GetComponent<Rigidbody>().AddForce(spawn.forward * fuerza);

                // Actualizar el tiempo en el que se podr� realizar el siguiente disparo
                ShotRateTime = Time.time + shotRate;

                // Destruir el proyectil despu�s de 2 segundos
                Destroy(NewBullet, 2);
            }
        }
    }
}
