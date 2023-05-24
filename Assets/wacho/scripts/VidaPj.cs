using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaPj : MonoBehaviour
{
    [SerializeField] int VidasPj; // Variable que representa las vidas del personaje
    [SerializeField] Slider VidasPantalla; // Referencia al slider de vidas en la pantalla


    private void Start()
    {
        VidasPantalla.maxValue = VidasPj; // Establecer el valor máximo del slider como las vidas del personaje
        VidasPantalla.value = VidasPantalla.maxValue; // Establecer el valor actual del slider al máximo de vidas
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            VidasPj--; // Reducir en 1 el número de vidas del personaje
            VidasPantalla.value = VidasPj; // Actualizar el valor del slider al número actual de vidas
        }

        if (VidasPj <= 0)
        {
            SceneManager.LoadScene("GameOver"); // Cargar la escena de Game Over si el personaje se queda sin vidas
        }
    }
}
