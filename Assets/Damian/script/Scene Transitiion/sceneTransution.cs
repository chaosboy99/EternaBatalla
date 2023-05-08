using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneTransution : MonoBehaviour
{
    public int Escena = 6; 

    public void OnTriggerEnter(Collider collision)
    {
        SceneManager.LoadScene(Escena);
    }
}
