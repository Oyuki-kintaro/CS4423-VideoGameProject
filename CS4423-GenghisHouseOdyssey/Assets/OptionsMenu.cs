using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider effectVolumeSlider;

    [Header("Resolution")]
    [SerializeField] TMP_Dropdown resDropdown;
    [SerializeField] Toggle fullscreenToggle;
    Resolution[] resolutions;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        GetResolutionOption();
        resDropdown.value = PlayerPrefs.GetInt("ResolutionDropdown");
        fullscreenToggle.isOn = PlayerPrefs.GetInt("FullscreenToggle", 1) == 1;
        Screen.SetResolution(resolutions[resDropdown.value].width,resolutions[resDropdown.value].height,fullscreenToggle.isOn);

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        
    }


    public void SetMasterVolume() {
       audioMixer.SetFloat("MasterVolume", ConvertToDec(masterVolumeSlider.value));
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume() {
        audioMixer.SetFloat("MusicVolume", ConvertToDec(musicVolumeSlider.value));
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void SetSoundEffectVolume() {
        audioMixer.SetFloat("EffectVolume", ConvertToDec(effectVolumeSlider.value));
        PlayerPrefs.SetFloat("EffectVolume", effectVolumeSlider.value);
        PlayerPrefs.Save();
    }

    float ConvertToDec(float sliderValue) {
        return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
    }

    void GetResolutionOption() {
        resDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        for(int i = 0; i < resolutions.Length; i++) {
            TMP_Dropdown.OptionData newOption;
            newOption = new TMP_Dropdown.OptionData(resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString());
            resDropdown.options.Add(newOption);
        }
    }

    public void ChooseResolution() {
        Screen.SetResolution(resolutions[resDropdown.value].width, resolutions[resDropdown.value].height, fullscreenToggle.isOn);
        PlayerPrefs.SetInt("ResolutionDropdown", resDropdown.value);
        PlayerPrefs.SetInt("FullscreenToggle", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OpenOptions() {
        canvas.enabled = true;
    }

    public void CloseOptions() {
        canvas.enabled = false;
        Debug.Log("close options");
    }

}
