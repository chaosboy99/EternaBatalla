using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueRoomCap1 : MonoBehaviour
{
    // Variable dondese guarda el TextMeshProUGUI para mostrar el di�logo
    public TextMeshProUGUI dialogueText;

    // Variable donde se guarda el nombre de los personajes izquierdo y derecho para cambiar sus carteles
    public GameObject NomPersonatgeEs, NomPersonatgeDret;

    // Array de l�neas de di�logo
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
        // Detectar el clic del rat�n para avanzar en el di�logo
        if (Input.GetMouseButtonDown(0))
        {
            // Si el texto mostrado es igual a la l�nea completa, avanzar a la siguiente l�nea
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            // Si el texto mostrado todav�a est� escribi�ndose, mostrar la l�nea completa inmediatamente
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
        // Dividir la l�nea en dos partes: el personaje y el texto
        string[] PersonatgeText = lines[index].Split("/");

        Debug.Log(PersonatgeText[0]);

        // Mostrar el cartel correspondiente seg�n el personaje indicado en la l�nea
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

        // Actualizar la l�nea para que contenga solo el texto (sin el personaje)
        lines[index] = PersonatgeText[1];

        // Mostrar el texto letra por letra con una velocidad espec�fica
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
            // Avanzar al siguiente �ndice de l�nea
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            // Si no hay m�s l�neas de di�logo, cargar la siguiente escena
            gameObject.SetActive(false);
            SceneManager.LoadScene("SceneSchool.Cap1");
        }
    }
}
