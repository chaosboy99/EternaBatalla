using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBrillo : MonoBehaviour
{
    public Slider _sliderBrillo;
    public float _sliderValueBrillo;
    public Image _panelBrillo;
    // Start is called before the first frame update
    void Start()
    {
        _sliderBrillo.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        _panelBrillo.color = new Color(_panelBrillo.color.r, _panelBrillo.color.g, _panelBrillo.color.b, _sliderBrillo.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSliderBrillo(float valor)
    {
        _sliderValueBrillo = valor;
        PlayerPrefs.SetFloat("brillo", _sliderValueBrillo);
        _panelBrillo.color = new Color(_panelBrillo.color.r, _panelBrillo.color.g, _panelBrillo.color.b, _sliderBrillo.value);
    }
}
