using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaPj : MonoBehaviour
{
    [SerializeField] int VidasPj;
    [SerializeField] Slider VidasPantalla;

    private void Start() 
    {

        VidasPantalla.maxValue = VidasPj;
        VidasPantalla.value = VidasPantalla.maxValue;

    }

    private void OnCollisionEnter(Collision other) 
    {
        

        if(other.gameObject.CompareTag("Enemigo"))
        {

          
            VidasPantalla.value = VidasPj;
            VidasPj--;


        }   
        if(VidasPj <= 0) {
            SceneManager.LoadScene("GameOver");
        }     

    }
    
}
