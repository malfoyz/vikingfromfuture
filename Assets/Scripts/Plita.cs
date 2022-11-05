using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plita : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")      // если объект столкновения имеет тег - Player
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
}
