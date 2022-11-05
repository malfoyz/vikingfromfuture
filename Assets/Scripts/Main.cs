using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public Image[] hearts;            // картинки сердечек
    public Sprite isLife, nonLife;    // изображения когда жизнь включена и не включена
    public GameObject PauseScreen;    // экран паузы
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public SoundEffector soundeffector;  // для вызова звуков при выигрыше и поражении
    public AudioSource soundSource;
    public void ReloadLvl()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Start()
    {
        soundSource.volume = (float)PlayerPrefs.GetInt("SoundVolume") / 9;
    }
    public void Update()
    {
        for (int i = 0; i < hearts.Length; i++)       // установка сердечек
        {
            if (player.GetHP() > i)               // если кол-во жизней у игрока больше, чем индекс данного сердечка в массиве
                hearts[i].sprite = isLife;
            else hearts[i].sprite = nonLife;
        }
    }
    public void PauseOn()
    {
        Time.timeScale = 0f;                            // останавливаем всё в игре
        player.enabled = false;                         // на всякий случай деактивируем компонент player
        PauseScreen.SetActive(true);                    // активация объекта PauseScreen
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }
    public void Win()               // интерфейс в случае победы
    {
        soundeffector.PlayWinSound();
        Time.timeScale = 0f;
        player.enabled = false;
        WinScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)    // если нет ключа Lvl (ни один уровень еще не пройден) или текущий уровень меньше, чем текущий активный (мы уже проходили этот уровень)
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);   // в качестве значения ключа будет индекс сцены
        print(PlayerPrefs.GetInt("Lvl"));
    }
    public void Lose()              // интерфейс в случае поражения
    {
        soundeffector.PlayLoseSound();
        Time.timeScale = 0f;
        player.enabled = false;
        LoseScreen.SetActive(true);
    }
    public void MenuLvl()           // выйти в меню
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene("Menu");
    }
    public void NextLvl()           // следующий уровень
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      // берем индекс текущей сцены, прибавляем 1 = следующая сцена за этой
    }
}
