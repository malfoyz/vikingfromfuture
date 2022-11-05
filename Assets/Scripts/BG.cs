using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour    //скрипт для движения фона
{
    float length, startpos;          // длина и стартовая позиция
    public GameObject cam;           // будет содержать в себе камеру
    public float parallaxEffect;     // насколько сильно будет смещаться фон ; чем больше, тем меньше расхождение; значения от 0 до 1
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;   // записываем длину нашего спрайта по оси X в значение length
    }

    
    void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);  // расхождение

        float dist = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length)  // если расхождение больше стартпоз + длины, то мы должны поменять кусок фона (который больше не видно) далее
            startpos += length;
        else if (temp < startpos - length)
            startpos -= length;
    }
}
