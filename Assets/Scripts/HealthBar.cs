using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;  // вектор, отвечающий за смещение позиции слайдера
    void Start()
    {
        
    }
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);  // parent потому что, слайдер - ребенок Canvas'a, а Canvas ребенок врага
    }
    public void SetHealthValue(int currentHealth, int maxHealth)
    {
        slider.gameObject.SetActive(currentHealth < maxHealth);  // активация health bar, если уровень жизней меньше, чем максимальный
        slider.value = currentHealth;
        slider.maxValue = maxHealth;
    }
}
