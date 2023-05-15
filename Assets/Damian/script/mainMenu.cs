using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Este método se llama cuando el jugador selecciona "Nuevo Juego" en el menú.
    public void newGame()
    {
        // Carga la escena con el índice 1 en el orden de las escenas del proyecto.
        SceneManager.LoadScene(1);
    }

    // Este método se llama cuando el jugador selecciona "Salir" en el menú.
    public void exitGame()
    {
        // Salir de la aplicación.
        Application.Quit();

        // Imprime un mensaje de depuración en la consola de Unity.
        Debug.Log("Saliendo de la aplicación");
    }
}
