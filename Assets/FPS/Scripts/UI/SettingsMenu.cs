using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


public class SettingsMenu : MonoBehaviour
{
    //making an audio mixer to control the volume
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    //an array for screen resolutions
    Resolution[] resolutions;

    //a start function for when the main menu starts
    void Start() 
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        //using a for loop to check for screen sizes and display them on the screen
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width+ " x " + resolutions[i].height;
            options.Add(option);

            //using if else statement to get the default screen size of the screen
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) 
            {
                currentResolutionIndex= i;
            }
        }
        resolutionDropdown.AddOptions(options);
        //ADDING THE DEFAULT resolution in the dropdown
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    //A function used for setting volume
    public void SetVolume (float volume) 
    {
        Debug.Log(volume);
        audioMixer.SetFloat ("volume", volume);
    }
    //a function to handle the screen size
   

    //a function for setting the full screen
    public void SetFullScreen (bool isFullScreen) 
    {
       Screen.fullScreen= isFullScreen;
    }

}
