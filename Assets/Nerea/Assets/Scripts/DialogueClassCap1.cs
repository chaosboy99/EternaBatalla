using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueClassCap1 : MonoBehaviour
{

    public TextMeshProUGUI dialogueText;

    public GameObject NomPersonatgeEs, NomPersonatgeDret;

    public string[] lines;

    public float vel_texto = 0.1f;

    int index;

    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }

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
        string[] PersonatgeText = lines[index].Split("/");

        Debug.Log(PersonatgeText[0]);

        if ("T" == PersonatgeText[0])
        {

            // Cartell dret
            NomPersonatgeDret.SetActive(true);
            NomPersonatgeEs.SetActive(false);

        }
        else if ("P" == PersonatgeText[0])
        {

            // Cartell Es
            NomPersonatgeEs.SetActive(true);
            NomPersonatgeDret.SetActive(false);

        }
        else if ("D" == PersonatgeText[0])
        {

            // Sense Cartells
            NomPersonatgeDret.SetActive(false);
            NomPersonatgeEs.SetActive(false);

        }

        lines[index] = PersonatgeText[1];

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
            SceneManager.LoadScene("SceneBaño.Cap1");

        }
    }


}
