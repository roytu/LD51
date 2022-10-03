using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MainMenuManager : MonoBehaviour
{

    public TMPro.TMP_Text logoMainText;
    public TMPro.TMP_Text logoShadowText;
    public AudioMixer mixer;

    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public Toggle fullscreenCheckbox;

    private Vector3 logoMainPos;
    private Vector3 logoShadowPos;

    // Start is called before the first frame update
    void Start()
    {
        if (logoMainText != null) {
            logoMainPos = logoMainText.rectTransform.localPosition;
        }
        if (logoShadowText != null) {
            logoShadowPos = logoShadowText.rectTransform.localPosition;
        }

        // Set sliders
        if (masterVolumeSlider != null) {
            float db;
            mixer.GetFloat("MasterVolume", out db);
            float volume = 1f - (db / -80f);
            masterVolumeSlider.SetValueWithoutNotify(volume);
        }
        if (sfxVolumeSlider != null) {
            float db;
            mixer.GetFloat("SFXVolume", out db);
            float volume = 1f - (db / -80f);
            sfxVolumeSlider.SetValueWithoutNotify(volume);
        }
        if (musicVolumeSlider != null) {
            float db;
            mixer.GetFloat("MusicVolume", out db);
            float volume = 1f - (db / -80f);
            musicVolumeSlider.SetValueWithoutNotify(volume);
        }
        if (fullscreenCheckbox != null) {
            fullscreenCheckbox.SetIsOnWithoutNotify(Screen.fullScreen);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (logoMainText != null) {
            Vector3 pos = logoMainPos;
            logoMainText.rectTransform.SetLocalPositionAndRotation(pos, Quaternion.identity);
        }
        if (logoShadowText != null) {
            Vector3 pos = logoShadowPos + new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                0f
            );
            logoShadowText.rectTransform.SetLocalPositionAndRotation(pos, Quaternion.identity);

            // Finick scale
            logoShadowText.GetComponent<TMPro.TextMeshProUGUI>().fontSize = Random.Range(120, 180);
        }
        
    }

    public void OnMasterVolumeChanged(float volume) {
        // 0 -> -80
        // 1 -> 0
        // (1 - x) * -80
        mixer.SetFloat("MasterVolume", (1 - volume) * -80f);
    }

    public void OnSFXVolumeChanged(float volume) {
        mixer.SetFloat("SFXVolume", (1 - volume) * -80f);
    }

    public void OnMusicVolumeChanged(float volume) {
        mixer.SetFloat("MusicVolume", (1 - volume) * -80f);
    }

    public void OnFullScreenChanged(bool fullscreen) {
        Screen.fullScreen = fullscreen;
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
