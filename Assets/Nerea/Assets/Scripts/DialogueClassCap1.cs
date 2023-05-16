using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueClassCap1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Referencia al componente TextMeshProUGUI para mostrar el diálogo en pantalla

    public GameObject NomPersonatgeEs, NomPersonatgeDret; // Referencias a los objetos de los carteles de personajes

    public string[] lines; // Arreglo de líneas de diálogo

    public float vel_texto = 0.1f; // Velocidad de escritura del texto

    int index; // Índice de la línea actual

    void Start()
    {
        dialogueText.text = string.Empty; // Inicializa el texto del diálogo como vacío
        StartDialogue(); // Inicia el diálogo
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index]) // Si se ha mostrado completamente la línea actual
            {
                NextLine(); // Muestra la siguiente línea
            }
            else
            {
                StopAllCoroutines(); // Detiene la escritura automática
                dialogueText.text = lines[index]; // Muestra la línea completa de forma instantánea
            }
        }
    }

    public void StartDialogue()
    {
        index = 0; // Inicializa el índice en 0

        StartCoroutine(WriteLine()); // Inicia la escritura de la primera línea
    }

    IEnumerator WriteLine()
    {
        string[] PersonatgeText = lines[index].Split("/"); // Divide la línea en dos partes: el personaje y el texto

        Debug.Log(PersonatgeText[0]); // Muestra el personaje en la consola (para depuración)

        if ("T" == PersonatgeText[0]) // Si el personaje es "T"
        {
            // Muestra el cartel del personaje de la derecha
            NomPersonatgeDret.SetActive(true);
            NomPersonatgeEs.SetActive(false);
        }
        else if ("P" == PersonatgeText[0]) // Si el personaje es "P"
        {
            // Muestra el cartel del personaje de la izquierda
            NomPersonatgeEs.SetActive(true);
            NomPersonatgeDret.SetActive(false);
        }
        else if ("D" == PersonatgeText[0]) // Si el personaje es "D"
        {
            // No muestra ningún cartel
            NomPersonatgeDret.SetActive(false);
            NomPersonatgeEs.SetActive(false);
        }

        lines[index] = PersonatgeText[1]; // Asigna solo el texto al arreglo de líneas

        foreach (char letter in lines[index].ToCharArray()) // Recorre cada letra del texto
        {
            dialogueText.text += letter; // Agrega la letra al texto del diálogo

            yield return new WaitForSeconds(vel_texto); // Espera un tiempo antes de mostrar la siguiente letra
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1) // Si no se ha alcanzado el final del diálogo
        {
            index++; // Incrementa el índice para mostrar la siguiente línea
            dialogueText.text = string.Empty; // Limpia el texto del diálogo
            StartCoroutine(WriteLine()); // Inicia la escritura de la siguiente línea
        }
        else
        {
            // Aqui se cierra el dialogo, por lo tanto aquí irá relacionado con la siguiente pantalla.
            gameObject.SetActive(false);
            SceneManager.LoadScene("SceneBaño.Cap1");

        }
    }


}
