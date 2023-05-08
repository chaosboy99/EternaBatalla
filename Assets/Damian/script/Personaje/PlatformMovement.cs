using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    // Esta variable define la altura máxima de movimiento de la caja.
    public int range = 10;

    // Esta variable define la posición central en el eje X de la caja.
    public float xCenter = 6f;

    // Creamos una referencia al componente Rigidbody de la caja.
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Creamos un Vector3 que define la posición a la que queremos mover la caja.
        // Utilizamos la función PingPong de la clase Mathf para generar un movimiento oscilatorio.
        // La función PingPong oscila entre 0 y el valor de la variable range.
        // Multiplicamos Time.time por 2 para que el movimiento sea más rápido.
        // Restamos range / 2f para que la posición oscile alrededor del valor de xCenter
        //Vector3 newPosition = new Vector3(xCenter + Mathf.PingPong(Time.time * 2, range) - range / 2f, transform.position.y, transform.position.z);
        // Movemos la caja a la nueva posición utilizando el método MovePosition del componente Rigidbody.
        //rb.MovePosition(newPosition);
        rb.MovePosition(new Vector3(xCenter + Mathf.PingPong(Time.time * 2, range) - range / 2f, transform.position.y, transform.position.z));
	}
}
