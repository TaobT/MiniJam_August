using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] private GameObject pauseGameObject;
    [SerializeField] private GameObject optionGameObject;
    public bool isPaused;

    [Header("Options")]
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private Slider SFXSlider;

    public static OptionsUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        } else
        {
            SetVolume();
            SetSFXVolume();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseGameObject.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Unpause();
        }
    }

    public void Unpause()
    {
        pauseGameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void BackButton()
    {
        pauseGameObject.SetActive(true);
        optionGameObject.SetActive(false);
    }

    public void OptionButton()
    {
        pauseGameObject.SetActive(false);
        optionGameObject.SetActive(true);
    }

    public void SetVolume()
    {
        float volume = musicSlider.value;
        musicMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        SFXMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetVolume();
        SetSFXVolume();
    }
}
