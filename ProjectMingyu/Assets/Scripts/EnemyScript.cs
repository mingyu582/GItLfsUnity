using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material table;
    public GameObject enemyBullet;
    private GameObject player;
    public int type;

    private int enemyScore;
    private float coin;
    private float speed = 1f;
    private float shotDelay;
    private float maxShotDelay = 1f;
    private float enemyHp = 5f;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        table = meshRenderer.material;
        player = GameObject.Find("Player");
        switch (type)
        {
            case 0:
                enemyHp = 5f; coin = 4f; enemyScore = 100;
                //enemyHp = 10; speed = 1.5f; coin = 3; maxShotTime = 3; shotSpeed = 3;
                break;
            case 1:
                enemyHp = 8f; coin = 8f; enemyScore = 500;
                break;
            case 2:
                enemyHp = 10f; coin = 20f; enemyScore = 1000;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime,Space.World);
        
        shotDelay += Time.deltaTime;
        if (shotDelay >= maxShotDelay && player != null)
        {
            Instantiate(enemyBullet,transform.position,Quaternion.identity);

            shotDelay = 0f;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("aaa");
        if (collision.gameObject.tag == "PlayerBullet") //플레이어총알
        {
            OnHit(5);
            print(enemyHp);
            if (enemyHp <= 0)
            {
                print("적 죽임");
                Destroy(collision.gameObject);
                Destroy(gameObject);
                PlayerScript playerScript = player.GetComponent<PlayerScript>();
                playerScript.score += enemyScore;
            }
        }
        else if (collision.gameObject.tag == "Player") //플레이어
        {
            Destroy(gameObject);
        }
    }
    public void OnHit(int dmg)
    {
        print("적 맞음");
        enemyHp -= dmg;
        meshRenderer.material = Resources.Load<Material>("Materials/Damaged");

        Invoke("ReturnMaterial", 0.1f);
        if (enemyHp <= 0)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.score += enemyScore;
            Destroy(gameObject);
        }
    }
    void ReturnMaterial()
    {
        meshRenderer.material = table;
        //meshRenderer.material = Resources.Load<Material>("Materials/Mine Sample 1");
    }
}
