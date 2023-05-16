using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueSchoolCap1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;    // Variable deonde se guarda TextMeshProUGUI que mostrará el diálogo

    public string[] lines;    // Array de líneas de diálogo

    public float vel_texto = 0.1f;    // Velocidad de escritura del texto

    int index;    // Variable donde se guarda el indice de las líneas de diálogo

    void Start()
    {
        dialogueText.text = string.Empty;    // Inicializa el texto del diálogo como vacío
        StartDialogue();    // Comienza el diálogo
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])    // Si se ha mostrado completamente la línea actual
            {
                NextLine();    // Avanza a la siguiente línea
            }
            else
            {
                StopAllCoroutines();    // Se para el array del dialogo
                dialogueText.text = lines[index];    // Muestra la línea completas
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;    // Inicializa el índice en 0 

        StartCoroutine(WriteLine());    // Comienza el array de las lineas  escribir el diálogo
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())    // Recorre cada letra de la línea actual
        {
            dialogueText.text += letter;    // Agrega la letra al texto del diálogo

            yield return new WaitForSeconds(vel_texto);    // Espera un tiempo antes de mostrar la siguiente letra
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)    // Si no es la última línea de diálogo
        {
            index++;    // Incrementa el índice para avanzar a la siguiente línea
            dialogueText.text = string.Empty;    // Limpia el texto del diálogo
            StartCoroutine(WriteLine());    // Comienza la corutina para escribir la siguiente línea
        }
        else
        {
            // Aquí se cierra el diálogo y se carga la siguiente escena (pantalla)
            gameObject.SetActive(false);    // Desactiva el objeto que contiene el diálogo
            SceneManager.LoadScene("SceneClass.Cap1");    // Carga la escena con el nombre "SceneClass.Cap1"
        }
    }
}
