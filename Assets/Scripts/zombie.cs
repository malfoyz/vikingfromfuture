using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;       // объект слежения = викинг
    Animator anim;
    public Transform groundCheck;    // точка
    bool isCollided;                     // столкнулся ли зомби со стеной?
    Rigidbody2D rb;
    int curHp;                     // текущее кол-во жизней
    public int maxHp = 4;                // макс. кол-во жизней
    public GameObject drop;
    bool canJump = true;                 // может прыгать
    public HealthBar healthBar;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        curHp = maxHp;
        healthBar.SetHealthValue(curHp, maxHp);
    }
    void Update()
    {
        CheckColliders();
        Flip();
        Run();
    }
    void Run()
    {
        if (!isCollided)
        {
            Vector3 position = target.position;    // берем позицию игрока по всем осям
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }
    }
    void Flip()
    {
        if (transform.position.x + 0.2f > target.position.x)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        if (transform.position.x - 0.2f < target.position.x)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void CheckColliders()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);

        isCollided = colliders.Length > 1;

        if (isCollided)   // если столкнулся слева или справа
        {
            anim.SetInteger("State", 0);           // анимация - стоит
            if (canJump)
            {
                rb.AddForce(transform.up * 8f, ForceMode2D.Impulse);
                canJump = false;
                StartCoroutine(Jump());
            }
        }
        else anim.SetInteger("State", 1);        // иначе бежит
    }
    public void RecountHp(int deltaHp)          // перерасчет жизней
    {
        curHp += deltaHp;
        healthBar.SetHealthValue(curHp, maxHp);   // изменение healt bar
        if (curHp <= 0)
        {
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        if (drop != null)
            Instantiate(drop, transform.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.3f);  // задержка для прыжка, чтобы не прыгал на человека слишком часто
        canJump = true;
    }
}
