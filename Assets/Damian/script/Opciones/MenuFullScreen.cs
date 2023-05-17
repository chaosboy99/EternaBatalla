using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class MenuFullScreen : MonoBehaviour
{
    public Toggle _toggleMenuFullScreen;

    public TMP_Dropdown _resolutionDropDown;
    Resolution[] _resolutionMenu;
    // Start is called before the first frame update
    void Start()
    {
        if(Screen.fullScreen)
        {
            _toggleMenuFullScreen.isOn = true;
        }
        else
        {
            _toggleMenuFullScreen.isOn = false;
        }
        checkResolution();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActiveFullScreen(bool _ActiveFullScreen)
    { 
        Screen.fullScreen = _ActiveFullScreen;
    }
    public void checkResolution() 
    {
        _resolutionMenu = Screen.resolutions;
        _resolutionDropDown.ClearOptions(); 
        List<string> resolutions = new List<string>();
        int _resolutionInitial = 0;

        for (int i =0; i <  _resolutionMenu.Length; i++)
        {
            string _optionMenu = _resolutionMenu[i].width + " x " + _resolutionMenu[i].height +"@ "+ _resolutionMenu[i].refreshRate + " Hz";
            resolutions.Add(_optionMenu);

            if (
                Screen.fullScreen && 
                _resolutionMenu[i].width == Screen.currentResolution.width && 
                _resolutionMenu[i].height == Screen.currentResolution.height
                )
            {
                _resolutionInitial = i;
            }
        }

        _resolutionDropDown.AddOptions(resolutions);
        _resolutionDropDown.value = _resolutionInitial;
        _resolutionDropDown.RefreshShownValue();

        _resolutionDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }
    public void changeResolutionMenu(int indexResolution)
    {
        PlayerPrefs.SetInt("numeroResolucion", _resolutionDropDown.value);

        Resolution resolution = _resolutionMenu[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
