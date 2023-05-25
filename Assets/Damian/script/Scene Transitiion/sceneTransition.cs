using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneTransition : MonoBehaviour
{
    public int Escena = 6;

    public void OnTriggerEnter(Collider collision)
    {
        // Reinicia o desactiva el componente CameraController
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            // Reiniciar el componente
            // cameraController.Restart(); // Reemplaza "Restart()" con el m�todo adecuado para reiniciar el componente CameraController

            // Desactivar el componente
            // cameraController.enabled = false;
        }

        // Carga la escena especificada por el n�mero de �ndice "Escena".
        SceneManager.LoadScene(Escena);
    }
}
