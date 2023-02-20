using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyBullet;
    private GameObject player;
    public int type;

    private float coin;
    private float speed = 1f;
    private float shotDelay;
    private float maxShotDelay = 1f;
    private float enemyHp = 5f;

    private void Start()
    {
        player = GameObject.Find("Player");
        switch (type)
        {
            case 0:
                enemyHp = 5f; coin = 4f;
                //enemyHp = 10; speed = 1.5f; coin = 3; maxShotTime = 3; shotSpeed = 3;
                break;
            case 1:
                enemyHp = 8f; coin = 8f;
                break;
            case 2:
                enemyHp = 10f; coin = 20f;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        
        shotDelay += Time.deltaTime;
        if (shotDelay >= maxShotDelay && player != null)
        {
            Instantiate(enemyBullet,transform.position,Quaternion.identity);

            shotDelay = 0f;
        }
    }
}
