using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Botones : MonoBehaviour
{
    public void PantallaInicio()
    {
        SceneManager.LoadScene(0);
    }

    public void VolverAJugar()
    {
        SceneManager.LoadScene(6);
    }
}
