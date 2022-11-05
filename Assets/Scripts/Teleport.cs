using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")      // если объект столкновения имеет тег - Player И враг не под уроном в это время
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
            Vector3 position1 = collision.gameObject.GetComponent<Transform>().position;
            Vector3 position2 = position1;
            position2.x += 10f;
            collision.gameObject.GetComponent<Transform>().position = Vector3.Lerp(position1, position2, 3f * Time.deltaTime);
            Destroy(gameObject);
        }
    }
}
