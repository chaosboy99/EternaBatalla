using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueBa�oCap1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;   //Variable donde se guarda al componente TextMeshProUGUI para mostrar el di�logo

    public GameObject NomPersonatgeEs, NomPersonatgeDret;  //Variable donde se guardan los nombres de los personajes

    public string[] lines;  // Array de l�neas de di�logo

    public float vel_texto = 0.1f;  // Velocidad a la que se muestra el texto del di�logo

    int index;  // �ndice donde se guardan las lineas del di�logo 

    void Start()
    {
        dialogueText.text = string.Empty;   // Aqui es donde Establece el texto del di�logo como vac�o
        StartDialogue();   // Inicia el di�logo
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))   // Verifica si se ha hecho clic con el bot�n izquierdo del mouse
        {
            if (dialogueText.text == lines[index])   // Verifica si se ha mostrado completamente la l�nea actual del di�logo
            {
                NextLine();   // Avanza a la siguiente l�nea del di�logo
            }
            else
            {
                StopAllCoroutines();   // Detiene todas las corutinas activas (en caso de que existan)
                dialogueText.text = lines[index];   // Muestra la l�nea completa de di�logo de inmediato
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;   // Establece el �ndice en 0 (comienza desde la primera l�nea del di�logo)
        StartCoroutine(WriteLine());   // Inicia la corutina para mostrar las l�neas del di�logo
    }

    IEnumerator WriteLine()
    {
        string[] PersonatgeText = lines[index].Split("/");   // Divide la l�nea de di�logo en dos partes: personaje y texto

        Debug.Log(PersonatgeText[0]);

        if ("T" == PersonatgeText[0])
        {
            // Si el personaje es "T" (cartel derecho)
            NomPersonatgeDret.SetActive(true);   // Activa el objeto de juego del cartel derecho
            NomPersonatgeEs.SetActive(false);   // Desactiva el objeto de juego del cartel izquierdo
        }
        else if ("C" == PersonatgeText[0])
        {
            // Si el personaje es "C-" (cartel izquierdo)
            NomPersonatgeEs.SetActive(true);   // Activa el objeto de juego del cartel izquierdo
            NomPersonatgeDret.SetActive(false);   // Desactiva el objeto de juego del cartel derecho
        }
        else if ("D" == PersonatgeText[0])
        {
            // Si el personaje es "D" (sin carteles)
            NomPersonatgeDret.SetActive(false);   // Desactiva el objeto de juego del cartel derecho
            NomPersonatgeEs.SetActive(false);   // Desactiva el objeto de juego del cartel izquierdo
        }

        lines[index] = PersonatgeText[1];   // Actualiza la l�nea de di�logo sin la etiqueta

        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(vel_texto);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }

        else
        {
            //Aqu� se cierra el dialogo, por lo tanto aqu� ir� relacionado con la siguiente pantalla.
            gameObject.SetActive(false);
            SceneManager.LoadScene(6);

        }
    }


}
