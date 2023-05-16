using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public GameObject bullet;    // Prefab del proyectil
    public Transform spawn;      // Transform de la posición de spawn del proyectil

    public float fuerza = 1500;  // Fuerza aplicada al proyectil
    public float shotRate = 0.5f;    // Tiempo mínimo entre disparos

    private float ShotRateTime = 0;  // Tiempo en el que se puede realizar el siguiente disparo

    // Update se llama una vez por frame
    void Update()
    {
        // Si se presiona el botón de disparo (por defecto, el botón izquierdo del ratón)
        if (Input.GetButtonDown("Fire1"))
        {
            // Verificar si ha pasado el tiempo mínimo entre disparos
            if (Time.time > ShotRateTime)
            {
                // Instanciar un nuevo proyectil en la posición y rotación de spawn
                GameObject NewBullet;
                NewBullet = Instantiate(bullet, spawn.position, spawn.rotation);

                // Aplicar una fuerza al proyectil en la dirección del spawn
                NewBullet.GetComponent<Rigidbody>().AddForce(spawn.forward * fuerza);

                // Actualizar el tiempo en el que se podrá realizar el siguiente disparo
                ShotRateTime = Time.time + shotRate;

                // Destruir el proyectil después de 2 segundos
                Destroy(NewBullet, 2);
            }
        }
    }
}
