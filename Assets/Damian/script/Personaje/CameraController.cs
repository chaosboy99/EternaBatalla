using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    // Transform del cuerpo del jugador
    public Transform body;

    // Valor vertical del movimiento del mouse
    private float _mouseVerticalValue;

    // Propiedad para obtener y establecer el valor vertical del movimiento del mouse
    private float MouseVerticalValue
    {
        get => _mouseVerticalValue;
        set
        {
            // Si el valor es 0, no se realiza ninguna acción
            if (value == 0) return;

            // Calcula el nuevo ángulo vertical sumando el valor actual y el nuevo valor
            float verticalAngle = _mouseVerticalValue + value;

            // Limita el ángulo vertical dentro del rango permitido
            verticalAngle = Mathf.Clamp(verticalAngle, -maxVerticalAngle, maxVerticalAngle);

            // Establece el nuevo ángulo vertical
            _mouseVerticalValue = verticalAngle;
        }
    }

    // Ángulo máximo de rotación vertical de la cámara
    public float maxVerticalAngle;

    // Sensibilidad de rotación de la cámara
    public float sensitivity;

    // Indica si se está en la escena GameOver o Win
    private bool isGameOverOrWinScene;

    // Método llamado en cada frame
    void Update()
    {
        // Si se está en la escena GameOver o Win, no se realiza ninguna acción
        if (isGameOverOrWinScene) return;

        // Obtiene el valor vertical del movimiento del mouse
        MouseVerticalValue = Input.GetAxis("Mouse Y");

        // Calcula la rotación final en base al movimiento vertical del mouse
        Quaternion finalRotation = Quaternion.Euler(-MouseVerticalValue * sensitivity, 0, 0);

        // Aplica la rotación a la transformación de la cámara
        transform.localRotation = finalRotation;

        // Rota el cuerpo horizontalmente en base al movimiento horizontal del mouse
        body.rotation = Quaternion.Euler(0, body.localRotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, 0);

        // Bloquea el cursor y lo oculta al hacer clic derecho
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        // Desbloquea el cursor y lo muestra al presionar la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Método para cambiar el estado de la escena GameOver o Win
    public void SetGameOverOrWinScene(bool value)
    {
        isGameOverOrWinScene = value;
    }
}
