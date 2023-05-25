using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueBañoCap1 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;   //Variable donde se guarda al componente TextMeshProUGUI para mostrar el diálogo

    public GameObject NomPersonatgeEs, NomPersonatgeDret;  //Variable donde se guardan los nombres de los personajes

    public string[] lines;  // Array de líneas de diálogo

    public float vel_texto = 0.1f;  // Velocidad a la que se muestra el texto del diálogo

    int index;  // Índice donde se guardan las lineas del diálogo 

    void Start()
    {
        dialogueText.text = string.Empty;   // Aqui es donde Establece el texto del diálogo como vacío
        StartDialogue();   // Inicia el diálogo
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))   // Verifica si se ha hecho clic con el botón izquierdo del mouse
        {
            if (dialogueText.text == lines[index])   // Verifica si se ha mostrado completamente la línea actual del diálogo
            {
                NextLine();   // Avanza a la siguiente línea del diálogo
            }
            else
            {
                StopAllCoroutines();   // Detiene todas las corutinas activas (en caso de que existan)
                dialogueText.text = lines[index];   // Muestra la línea completa de diálogo de inmediato
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;   // Establece el índice en 0 (comienza desde la primera línea del diálogo)
        StartCoroutine(WriteLine());   // Inicia la corutina para mostrar las líneas del diálogo
    }

    IEnumerator WriteLine()
    {
        string[] PersonatgeText = lines[index].Split("/");   // Divide la línea de diálogo en dos partes: personaje y texto

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

        lines[index] = PersonatgeText[1];   // Actualiza la línea de diálogo sin la etiqueta

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
            //Aquí se cierra el dialogo, por lo tanto aquí irá relacionado con la siguiente pantalla.
            gameObject.SetActive(false);
            SceneManager.LoadScene(6);

        }
    }


}
