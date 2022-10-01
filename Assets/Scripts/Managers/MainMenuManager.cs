using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMasterVolumeChanged(float volume) {
    }

    public void OnSFXVolumeChanged(float volume) {
    }

    public void OnMusicVolumeChanged(float volume) {
    }

    public void OnPlayButtonPressed() {
        SceneManager.LoadScene("Game");
    }

    public void OnSettingsButtonPressed() {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void OnCreditsButtonPressed() {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void OnBackButtonPressed() {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitPressed() {
        Application.Quit();
    }
}
