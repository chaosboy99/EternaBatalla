using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueRoomCap1 : MonoBehaviour
{
    // Variable dondese guarda el TextMeshProUGUI para mostrar el diálogo
    public TextMeshProUGUI dialogueText;

    // Variable donde se guarda el nombre de los personajes izquierdo y derecho para cambiar sus carteles
    public GameObject NomPersonatgeEs, NomPersonatgeDret;

    // Array de líneas de diálogo
    public string[] lines;

    // Velocidad de escritura del texto
    public float vel_texto = 0.1f;

    // variable donde se guarda el indice de lineas del dialogo
    int index;

    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        // Detectar el clic del ratón para avanzar en el diálogo
        if (Input.GetMouseButtonDown(0))
        {
            // Si el texto mostrado es igual a la línea completa, avanzar a la siguiente línea
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            // Si el texto mostrado todavía está escribiéndose, mostrar la línea completa inmediatamente
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        // Dividir la línea en dos partes: el personaje y el texto
        string[] PersonatgeText = lines[index].Split("/");

        Debug.Log(PersonatgeText[0]);

        // Mostrar el cartel correspondiente según el personaje indicado en la línea
        if ("T" == PersonatgeText[0])
        {
            // Cartel derecho
            NomPersonatgeDret.SetActive(true);
            NomPersonatgeEs.SetActive(false);
        }
        else if ("M" == PersonatgeText[0])
        {
            // Cartel izquierdo
            NomPersonatgeEs.SetActive(true);
            NomPersonatgeDret.SetActive(false);
        }
        else if ("D" == PersonatgeText[0])
        {
            // Sin carteles
            NomPersonatgeDret.SetActive(false);
            NomPersonatgeEs.SetActive(false);
        }

        // Actualizar la línea para que contenga solo el texto (sin el personaje)
        lines[index] = PersonatgeText[1];

        // Mostrar el texto letra por letra con una velocidad específica
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
            // Avanzar al siguiente índice de línea
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            // Si no hay más líneas de diálogo, cargar la siguiente escena
            gameObject.SetActive(false);
            SceneManager.LoadScene("SceneSchool.Cap1");
        }
    }
}
