using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timer = 0;         // Variable que almacena el tiempo restante del temporizador
    public Text textoTimer;        // Variable donde se guarda el Text que muestra el temporizador en pantalla

    void Update()
    {
        timer -= Time.deltaTime;   // Resta el tiempo transcurrido desde el último frame al temporizador

        textoTimer.text = "" + timer.ToString("f0");  // Actualiza el texto del componente Text con el valor del temporizador, formateado sin decimales

        if (timer <= 0)
        {
            SceneManager.LoadScene("GameOver");   // Si el temporizador llega o se pasa de cero, carga la escena llamada "GameOver"
        }
    }
}
