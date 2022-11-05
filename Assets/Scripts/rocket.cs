using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    private float speed = 10f;
    
    private void Start()
    {
        StartCoroutine(Death());
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);                           // полет пули
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);     // на переднем плане
    }
    // обработаем столкновения
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
