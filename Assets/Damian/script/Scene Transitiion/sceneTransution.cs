using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneTransition : MonoBehaviour
{
    public int Escena = 6;

    public void OnTriggerEnter(Collider collision)
    {
        // Carga la escena especificada por el n�mero de �ndice "Escena".
        SceneManager.LoadScene(Escena);
    }
}
