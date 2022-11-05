using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;                 // физика героя
    public float speed;             // скорость передвижения
    public float jumpHeight;        // высота прыжка
    public Transform groundCheck;   // точка у ног героя, проверяющая землю под ногами
    bool isGrounded;                // приземлен ли герой?
    Animator anim;                  // компонент, переключающий анимацию
    int curHp;                      // текущее кол-во жизней
    int maxHp = 3;                  // макс. кол-во жизней
    bool isHit = false;             // персонаж под ударом?
    public Main main;               // сюда закинем объект камеры в Unity
    public Transform shootPosition;
    public GameObject rocket;       // объект пули
    private bool canShoot = true;          // для задержки между выстрелами
    public SoundEffector soundeffector;    // экземпляр для вызова методов вызова звуков
    public bool isCanToBeInjured = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHp;
    }
    void Update()
    {
        CheckGround();
        if (Input.GetAxis("Horizontal") == 0 && isGrounded)    // регулирование анимации
            anim.SetInteger("State", 1);
        else if (isGrounded)
        {
            Flip();
            anim.SetInteger("State", 2);
        }

        if (transform.position.y < -30f)                      // смерть в случае падения
            RecountHp(-3);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canShoot)      // создание пули при выстреле
            StartCoroutine(Shoot());
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)   // если нажат пробел и он приземлен
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            soundeffector.PlayJumpSound();
        }
    }
    private IEnumerator Shoot()                       // задержка для выстрела
    {
        canShoot = false;
        soundeffector.PlayTheShot();
        Instantiate(rocket, shootPosition.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
    public IEnumerator WaitForBeat()                       // задержка для отнятия жизней
    {
        yield return new WaitForSeconds(1f);
        isCanToBeInjured = true;
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.04f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
            anim.SetInteger("State", 3);
    }
    public void RecountHp(int deltaHp)
    {
        curHp += deltaHp;
        if (deltaHp < 0)
            StopCoroutine(OnHit());
            isHit = true;                // персонажа бьют
            StartCoroutine(OnHit());
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
        }
    }
    IEnumerator OnHit()   // корутина
    {
        if (isHit)     // персонажа бьют => отнимаем цвет
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.04f, GetComponent<SpriteRenderer>().color.b - 0.04f);
        else GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.04f, GetComponent<SpriteRenderer>().color.b + 0.04f);

        if (GetComponent<SpriteRenderer>().color.g == 1f)
            StopCoroutine(OnHit());
        if (GetComponent<SpriteRenderer>().color.g <= 0)   // если зеленый (а значит и голубой) цвет уже = 0
            isHit = false;                                 // то перестаем отнимать цвет, раз его не бьют

        yield return new WaitForSeconds(0.02f);   // периодичность корутины (ожидание)
        StartCoroutine(OnHit());
    }
    void Lose()
    {
        main.GetComponent<Main>().Lose();
    }
    public int GetHP()
    {
        return curHp;
    }
}
