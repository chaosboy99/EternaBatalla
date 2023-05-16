using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueSchoolCap1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;    // Variable deonde se guarda TextMeshProUGUI que mostrar� el di�logo

    public string[] lines;    // Array de l�neas de di�logo

    public float vel_texto = 0.1f;    // Velocidad de escritura del texto

    int index;    // Variable donde se guarda el indice de las l�neas de di�logo

    void Start()
    {
        dialogueText.text = string.Empty;    // Inicializa el texto del di�logo como vac�o
        StartDialogue();    // Comienza el di�logo
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])    // Si se ha mostrado completamente la l�nea actual
            {
                NextLine();    // Avanza a la siguiente l�nea
            }
            else
            {
                StopAllCoroutines();    // Se para el array del dialogo
                dialogueText.text = lines[index];    // Muestra la l�nea completas
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;    // Inicializa el �ndice en 0 

        StartCoroutine(WriteLine());    // Comienza el array de las lineas  escribir el di�logo
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())    // Recorre cada letra de la l�nea actual
        {
            dialogueText.text += letter;    // Agrega la letra al texto del di�logo

            yield return new WaitForSeconds(vel_texto);    // Espera un tiempo antes de mostrar la siguiente letra
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)    // Si no es la �ltima l�nea de di�logo
        {
            index++;    // Incrementa el �ndice para avanzar a la siguiente l�nea
            dialogueText.text = string.Empty;    // Limpia el texto del di�logo
            StartCoroutine(WriteLine());    // Comienza la corutina para escribir la siguiente l�nea
        }
        else
        {
            // Aqu� se cierra el di�logo y se carga la siguiente escena (pantalla)
            gameObject.SetActive(false);    // Desactiva el objeto que contiene el di�logo
            SceneManager.LoadScene("SceneClass.Cap1");    // Carga la escena con el nombre "SceneClass.Cap1"
        }
    }
}
