using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    // Transform de la cámara
    public Transform cameraTransform;

    // Ángulo máximo de rotación vertical de la cámara
    public float maxVerticalAngle;

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

    // Sensibilidad de rotación de la cámara
    public float sensitivity;

    // Variable para almacenar el estado de la escena
    private bool isGameOverOrWin;

    // Método llamado en cada frame
    void Update()
    {
        // Verifica si la escena actual es "gameover" o "win"
        if (SceneManager.GetActiveScene().name == "gameover" || SceneManager.GetActiveScene().name == "win")
        {
            // Desactiva el movimiento de la cámara
            isGameOverOrWin = true;
            return;
        }
        else
        {
            // Activa el movimiento de la cámara
            isGameOverOrWin = false;
        }

        // Si el movimiento de la cámara está desactivado, no se realiza ninguna acción
        if (isGameOverOrWin) return;

        // Obtiene el valor vertical del movimiento del mouse
        MouseVerticalValue = Input.GetAxis("Mouse Y");

        // Calcula la rotación final en base al movimiento vertical del mouse
        Quaternion finalRotation = Quaternion.Euler(-MouseVerticalValue * sensitivity, 0, 0);

        // Aplica la rotación a la transformación de la cámara
        cameraTransform.localRotation = finalRotation;

        // Rota el cuerpo horizontalmente en base al movimiento horizontal del mouse
        body.rotation = Quaternion.Euler(0, body.localRotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, 0);

        // Bloquea el cursor y lo oculta al hacer clic derecho
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Desbloquea el cursor y lo muestra al presionar la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
