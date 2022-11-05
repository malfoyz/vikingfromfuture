using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 1f;         // скорость
    bool isWait = false;             // ждет ли, чтобы вылезти из под земли?
    bool isHidden = false;            // рука под землей?
    public float waitTime = 1f;      // время ожидания
    public Transform point;          // будет означать, куда он должен выпрыгивать, а потом куда скрываться

    void Start()
    {
        point.transform.position = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);    // точке присваиваем позицию = позиция руки + 1 по ОY
    }
    void Update()
    {
        if (isWait == false)
            transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);    // если рука не ждет - двигаем его в точку
        if (transform.position == point.position)                                                           // если позиция руки дошла до позиции точки
        {
            if (isHidden)                                                                // и если рука была скрыта --> меняем точку на позицию выше
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);
                isHidden = false;                                                        // рука больше не скрыта
            }
            else                                                                         // иначе меняем точку на позицию ниже
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y - 4f, transform.position.z);
                isHidden = true;                                                         // рука скрыта
            }
            isWait = true;
            StartCoroutine(Waiting());
        }
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);   // ждем определенное кол-во секунд
        isWait = false;                              // больше не ждем --> действуем
    }
}
