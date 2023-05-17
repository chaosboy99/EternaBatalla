using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuVolumen : MonoBehaviour
{
    public Slider _sliderVolumen;
    public float _sliderValue;
    public Image _imageMute;
    // Start is called before the first frame update
    void Start()
    {
        _sliderVolumen.value = PlayerPrefs.GetFloat("VolumenAudio", 0.5f);
        AudioListener.volume = _sliderValue;
        checkIfMute();

    }
    public void changeListener(float valor) 
    {
        _sliderValue = valor;
        PlayerPrefs.SetFloat("VolumenAudio", _sliderValue);
        AudioListener.volume = _sliderValue; 
        checkIfMute();
    }
    public void checkIfMute()
    {
        if(_sliderValue == 0)

        {
            _imageMute.enabled = true;
        } else
        {
            _imageMute.enabled = false;
        }
    }
}
