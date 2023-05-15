using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textDisplayTutorial : MonoBehaviour
{
    public GameObject _UiObject;

    // Start is called before the first frame update
    void Start()
    {
        // Desactiva el objeto de interfaz de usuario al inicio del juego.
        _UiObject.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {
        // Verifica si el jugador con etiqueta "Player" ha colisionado con este objeto.
        if (player.gameObject.tag == "Player")
        {
            // Activa el objeto de interfaz de usuario.
            _UiObject.SetActive(true);
            // Inicia la funcion de tiempo de espera.
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec()
    {
        // Genera una pausa de 5 segundos.
        yield return new WaitForSeconds(5);
        // Desactiva el objeto de interfaz de usuario.
        _UiObject.SetActive(false);
        // Destruye el objeto creadoo
        Destroy(gameObject);
    }
}
