using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Este m�todo se llama cuando el jugador selecciona "Nuevo Juego" en el men�.
    public void newGame()
    {
        // Carga la escena con el �ndice 1 en el orden de las escenas del proyecto.
        SceneManager.LoadScene(1);
    }

    // Este m�todo se llama cuando el jugador selecciona "Salir" en el men�.
    public void exitGame()
    {
        // Salir de la aplicaci�n.
        Application.Quit();

        // Imprime un mensaje de depuraci�n en la consola de Unity.
        Debug.Log("Saliendo de la aplicaci�n");
    }
}
