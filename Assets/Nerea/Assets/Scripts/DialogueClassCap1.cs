using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueClassCap1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Referencia al componente TextMeshProUGUI para mostrar el di�logo en pantalla

    public GameObject NomPersonatgeEs, NomPersonatgeDret; // Referencias a los objetos de los carteles de personajes

    public string[] lines; // Arreglo de l�neas de di�logo

    public float vel_texto = 0.1f; // Velocidad de escritura del texto

    int index; // �ndice de la l�nea actual

    void Start()
    {
        dialogueText.text = string.Empty; // Inicializa el texto del di�logo como vac�o
        StartDialogue(); // Inicia el di�logo
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index]) // Si se ha mostrado completamente la l�nea actual
            {
                NextLine(); // Muestra la siguiente l�nea
            }
            else
            {
                StopAllCoroutines(); // Detiene la escritura autom�tica
                dialogueText.text = lines[index]; // Muestra la l�nea completa de forma instant�nea
            }
        }
    }

    public void StartDialogue()
    {
        index = 0; // Inicializa el �ndice en 0

        StartCoroutine(WriteLine()); // Inicia la escritura de la primera l�nea
    }

    IEnumerator WriteLine()
    {
        string[] PersonatgeText = lines[index].Split("/"); // Divide la l�nea en dos partes: el personaje y el texto

        Debug.Log(PersonatgeText[0]); // Muestra el personaje en la consola (para depuraci�n)

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
            // No muestra ning�n cartel
            NomPersonatgeDret.SetActive(false);
            NomPersonatgeEs.SetActive(false);
        }

        lines[index] = PersonatgeText[1]; // Asigna solo el texto al arreglo de l�neas

        foreach (char letter in lines[index].ToCharArray()) // Recorre cada letra del texto
        {
            dialogueText.text += letter; // Agrega la letra al texto del di�logo

            yield return new WaitForSeconds(vel_texto); // Espera un tiempo antes de mostrar la siguiente letra
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1) // Si no se ha alcanzado el final del di�logo
        {
            index++; // Incrementa el �ndice para mostrar la siguiente l�nea
            dialogueText.text = string.Empty; // Limpia el texto del di�logo
            StartCoroutine(WriteLine()); // Inicia la escritura de la siguiente l�nea
        }
        else
        {
            // Aqui se cierra el dialogo, por lo tanto aqu� ir� relacionado con la siguiente pantalla.
            gameObject.SetActive(false);
            SceneManager.LoadScene("SceneBa�o.Cap1");

        }
    }


}
