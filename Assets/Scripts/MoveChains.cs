using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChains : MonoBehaviour
{
    public Transform player;
    float speed = 2f;
    public Transform target;
    public GameObject bigPlita;
    public void Start()
    {
        target.transform.position = new Vector3(transform.position.x, transform.position.y + 2.53f, transform.position.z);
    }
    public void Update()
    {
        if (player.transform.position.y < -6 && player.transform.position.x > 91)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            bigPlita.SetActive(false);
        }
    }
}
