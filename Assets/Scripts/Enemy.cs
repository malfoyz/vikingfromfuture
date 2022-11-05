using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")                                                                   // если объект столкновения имеет тег - Player
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 8f, ForceMode2D.Impulse);
            if (collision.gameObject.GetComponent<Player>().isCanToBeInjured)
            {
                collision.gameObject.GetComponent<Player>().RecountHp(-1);                                           //возьмем компонент-скрипт Player этого объекта, через который вызываем метод перерасчета жизней, отняв 1 жизнь
                collision.gameObject.GetComponent<Player>().isCanToBeInjured = false;
                StartCoroutine(collision.gameObject.GetComponent<Player>().WaitForBeat());
            }
        }
        else if (collision.gameObject.tag == "Rocket")                                                               // иначе если это пуля викинга
        {
            GetComponent<zombie>().RecountHp(-1);
        }
    }
}
