using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public AudioClip shotClip;
    public AudioClip damagedClip;
    MeshRenderer meshRenderer;
    public GameObject playerBulletPrefabA;
    public GameObject playerBulletPrefabB;
    public Transform[] shotPoint;
    public GameObject manager;
    public GameObject bookShot;

    private float horizontal;
    private float vertical;
    private float speed = 10f;

    public float shotDelay;
    public float maxShotDelay = 0.2f;

    private Vector2 playerSize;
    private Vector3 min,max;
    //상 y
    //하 -y
    //좌 -x
    //우 x
    public int score;
    public int hp = 100;
    public int pain = 0;
    private int power = 1;
    private int maxPower = 4;

    public bool isRespawnTime;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        playerSize = gameObject.GetComponent<MeshRenderer>().bounds.size/2;
        //print(playerSize);
    }
    private void Update()
    {
        PlayerMove();
        shotDelay += Time.deltaTime;
        Fire();
    }

    void Fire()
    {
        if (!Input.GetKey(KeyCode.J))
        {
            return;
        }
        if (shotDelay < maxShotDelay)
        {
            return;
        }
        SoundManager.instance.SFXPlay("PlayerShot", shotClip);
        switch (power)
        {
            case 1:
                Instantiate(playerBulletPrefabA, shotPoint[1].position, Quaternion.Euler(-90.0f, 0, 0));
                break;
            case 2:
                Instantiate(playerBulletPrefabA, shotPoint[0].position, Quaternion.Euler(-90.0f, 0, 0));
                Instantiate(playerBulletPrefabA, shotPoint[2].position, Quaternion.Euler(-90.0f, 0, 0));
                break;
            case 3:
                Instantiate(playerBulletPrefabA, shotPoint[0].position, Quaternion.Euler(-90.0f, 0, 0));
                Instantiate(playerBulletPrefabB, shotPoint[2].position, Quaternion.Euler(-90.0f, 0, 0));
                break;
            case 4:
                Instantiate(playerBulletPrefabB, shotPoint[0].position, Quaternion.Euler(-90.0f, 0, 0));
                Instantiate(playerBulletPrefabB, shotPoint[2].position, Quaternion.Euler(-90.0f, 0, 0));
                break;
        }
        shotDelay = 0f;
    }
    

    void PlayerMove()
    {
        //print("playerSize: " + playerSize);
        float newX;
        float newY;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, vertical, 0).normalized;
        transform.position += dir * Time.deltaTime * speed;

        newX = transform.position.x;
        newY = transform.position.y;

        newX = Mathf.Clamp(newX, min.x + playerSize.x, max.x - playerSize.x);
        newY = Mathf.Clamp(newY, min.y + playerSize.y, max.y - playerSize.y);
        //print(newX + " : " + newY);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isRespawnTime) //무적 시간이면 적에게 맞지 않음
            return;
        GameManager managerScript = manager.GetComponent<GameManager>();

        if (collision.gameObject.tag == "Enemy")
        {
            SoundManager.instance.SFXPlay("PlayerDamaged", damagedClip);
            hp -= 15;
            managerScript.UpdateHpSlider(hp);
            if (hp <= 0)
            {
                print("비행기 파괴됨");
                print(score);
                DataManager.curScore = score;
                managerScript.GameOver(score);
                Destroy(gameObject);
            }
            else
            {
                OnDamaged();
                Invoke("OnDamaged",1.5f);
            }
            GameObject enemyObject = collision.gameObject;
            EnemyScript enemyScript = enemyObject.GetComponent<EnemyScript>();
            if (enemyScript.type != 3)
            {
                Destroy(collision.gameObject);
            }
            
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            SoundManager.instance.SFXPlay("PlayerDamaged", damagedClip);
            hp -= 15;
            managerScript.UpdateHpSlider(hp);
            if (hp <= 0)
            {
                print("비행기 파괴됨");
                print(score);
                DataManager.curScore = score;
                managerScript.GameOver(score);
                Destroy(gameObject);
            }
            else
            {
                OnDamaged();
                Invoke("OnDamaged", 1.5f);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Item")
        {
            ItemScript itemScript = collision.gameObject.GetComponent<ItemScript>();
            switch (itemScript.type)
            {
                case "book":
                    bookShot.SetActive(true);
                    Invoke("OffBookShot", 4f);
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        EnemyScript enemyScript = enemies[i].GetComponent<EnemyScript>();
                        if (enemyScript.type == 3)
                        {
                            return;
                        }
                        else
                        {
                            enemyScript.OnHit(1000);
                        }
                        
                    }

                    GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        Destroy(bullets[i]);
                    }
                    break;
                case "healPotion":
                    if (hp >= 80)
                    {
                        hp = 100;
                        managerScript.UpdateHpSlider(hp);
                    }
                    else
                    {
                        hp += 20;
                        managerScript.UpdateHpSlider(hp);
                    }
                    break;
                case "powerUp":
                    if (power != maxPower)
                    {
                        power++;
                    }
                    break;
                case "painkiller":
                    if (pain >= 20)
                    {
                        pain -= 20;
                        managerScript.UpdatePainSlider(pain);
                    }
                    else
                    {
                        pain = 0;
                        managerScript.UpdatePainSlider(pain);
                    }
                    break;
                case "sheild":
                    break;
                case "supportRobot":
                    break;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "RedBlood")
        {
            SoundManager.instance.SFXPlay("PlayerDamaged", damagedClip);
            pain += 20;
            managerScript.UpdatePainSlider(pain);
            Destroy(collision.gameObject);
            if (pain <= 0)
            {
                print("비행기 파괴됨");
                print(score);
                DataManager.curScore = score;
                managerScript.GameOver(score);
                Destroy(gameObject);
            }
            else
            {
                OnDamaged();
                Invoke("OnDamaged", 1.5f);
            }
        }


    }

    void OffBookShot()
    {
        bookShot.SetActive(false);
    }


    void OnDamaged()
    {
        //플레이어 깜빡거림 무적시간 3초
        isRespawnTime = !isRespawnTime;
        if (isRespawnTime) //무적 타임 이펙트 (투명)
        {
            meshRenderer.material = Resources.Load<Material>("Materials/Damaged");
        }
        else
        {
            meshRenderer.material = Resources.Load<Material>("Materials/StarSparrow_White"); //무적 타임 종료(원래대로)
        }
    }

}
