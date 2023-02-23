using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlood : MonoBehaviour
{
    private float speed = 2f;
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }
    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (player != null) { return; }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.pain += 20;
            Destroy(gameObject);
        }
    }
}
