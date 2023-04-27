using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransution : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision)
    {
        SceneManager.LoadScene(6);
    }
}
