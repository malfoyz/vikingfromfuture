using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button[] lvls;       // массив кнопок (уровней) в меню
    public Slider soundSlider;  // ползунок громкости
    public Text soundText;      // цифра на ползунке
    void Start()
    {
        if (PlayerPrefs.HasKey("Lvl"))                   // если хотя бы 1-ый уровень пройден
            for (int i = 0; i < lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    lvls[i].interactable = true;
                else lvls[i].interactable = false;
            }

        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetInt("SoundVolume", 8);

        soundSlider.value = PlayerPrefs.GetInt("SoundVolume");     // задали громкость звука в начале
    }

    void Update()
    {
        PlayerPrefs.SetInt("SoundVolume", (int)soundSlider.value);
        soundText.text = soundSlider.value.ToString();
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }
    public void MenuQuit()
    {
        Application.Quit();
    }
}
