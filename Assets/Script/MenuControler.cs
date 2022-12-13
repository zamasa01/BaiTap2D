using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuControler : MonoBehaviour
{
    public Slider volumeSlider;
    public Text volumeValue;
    public Slider RocketSpeedSlider;
    public Text RocketSpeedValue;
    public Animator animator;
    public GameObject RocketToy;
    public AudioSource Music1;
    public AudioSource Music2;
    public AudioSource Music3;
    public Dropdown DropdownMusicList;
    public Dropdown DropdownModeRKList;
    private string AddressDataFile = @"Assets\Data\SettingInfo.csv";

    public void Start()
    {
        LoadValue();
        animator.GetComponent<Animator>();
    }
    public void ShowSetting()
    {
        Debug.Log("Funtion Called");
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Empty") || animator.GetCurrentAnimatorStateInfo(0).IsName("SettingScene_End"))
        {
            Debug.Log("Animation show is on");
            animator.SetTrigger("show");
        }
        else
        {
            Debug.Log("Animation show is off");
            animator.SetTrigger("unshow");
        }
    }
    public void ShowVolumeValue()
    {
        //Set audio text number
        volumeValue.text = volumeSlider.value.ToString("0");
        AudioListener.volume = volumeSlider.value/100f;
    }
    public void ShowRocketSpeedValue()
    {
        //Set speed text number
        RocketSpeedValue.text = RocketSpeedSlider.value.ToString("0");
        float sp = Mathf.Lerp(1f, 4f, RocketSpeedSlider.value / 100f);
        RocketToy.GetComponent<RocketFollow>().speed = sp;
    }
    public void ButtonSaveValue()
    {
        //PlayerPrefs.SetFloat("ValueVolume", volumeSlider.value);
        WriteData(AddressDataFile);
    }
    public void LoadValue()
    {
        //volumeSlider.value = PlayerPrefs.GetFloat("ValueVolume");
        ReadData(AddressDataFile);
        ShowVolumeValue();
        ShowRocketSpeedValue();
        DropdownMusic();
        DropdownModeRocket();
    }
    public void ButtonPlayGame()
    {
        Debug.Log("Triggle Animator is on");
        animator.SetTrigger("PlayGame_On");
        //Use Invoke() to wait for animation 2 secon
        Invoke("LoadScene", 2f);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("MainGame_Scene");
    }
    public void DropdownMusic()
    {
        int value = DropdownMusicList.value;
        switch (value)
        {
            case 0:
                {
                    Music1.Stop();
                    Music2.Stop();
                    Music3.Stop();
                    break;
                }
            case 1:
                {
                    Music1.Play();
                    Music2.Stop();
                    Music3.Stop();
                    break;
                }
            case 2:
                {
                    Music1.Stop();
                    Music2.Play();
                    Music3.Stop();
                    break;
                }
            case 3:
                {
                    Music1.Stop();
                    Music2.Stop();
                    Music3.Play();
                    break;
                }
        }
    }
    public void DropdownModeRocket()
    {
        RocketToy.GetComponent<RocketFollow>().RocketFlyMode = DropdownModeRKList.value;
    }
    public void ReadData(string address)
    {
        StreamReader ReadFile = new StreamReader(address);
        string[] SplitData = ReadFile.ReadToEnd().Split(',', '\n');
        volumeSlider.value = float.Parse(SplitData[0]);
        DropdownMusicList.value = int.Parse(SplitData[1]);
        RocketSpeedSlider.value = float.Parse(SplitData[2]);
        DropdownModeRKList.value = int.Parse(SplitData[3]);
        ReadFile.Close();
    }
    public void WriteData(string address)
    {
        StreamWriter WriteFile = new StreamWriter(address);
        WriteFile.WriteLine($"{volumeSlider.value},{DropdownMusicList.value},{RocketSpeedSlider.value},{DropdownModeRKList.value}");
        WriteFile.Close();
    }
}
