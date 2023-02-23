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
    private float enemyHp;
    private bool isBoss = false;
    private float timer;
    private float timer2;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    private bool isPatternEnd = true;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        table = meshRenderer.material;
        player = GameObject.Find("Player");
        switch (type)
        {
            case 0:
                enemyHp = 10f; coin = 4f; enemyScore = 100;
                break;
            case 1:
                enemyHp = 8f; coin = 8f; enemyScore = 500;
                break;
            case 2:
                enemyHp = 10f; coin = 20f; enemyScore = 1000;
                break;
            case 3:
                enemyHp = 100f; coin = 100f; enemyScore = 5000;
                isBoss = true;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        EnemyMove(isBoss);
        if (isBoss == true)
        {
            if (isPatternEnd)
            {
                Invoke("Term", 3f);
            }
            return;
        }
        

        shotDelay += Time.deltaTime;
        if (shotDelay >= maxShotDelay && player != null)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);

            shotDelay = 0f;
        }
    }
    void Term()
    {
        patternIndex = Random.Range(0, 4);
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                CreateEnemy();
                break;
            case 2:
                FireArround1();
                break;
            case 3:
                FireAround2();
                break;
        }
        isPatternEnd = false;
    }
    void FireForward()
    {
        print("�ι߾� �÷��̾� ��������");

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 0.3f);
        }
        if (curPatternCount == maxPatternCount[patternIndex])
        {
            isPatternEnd = true;
        }
    }
    void CreateEnemy()
    {
        print("����ȯ");

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("CreateEnemy", 1f);
        }
        if (curPatternCount == maxPatternCount[patternIndex])
        {
            isPatternEnd = true;
        }
    }
    void FireArround1()
    {
        print("�Ѹ���");

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArround1", 0.4f);
        }
        if (curPatternCount == maxPatternCount[patternIndex])
        {
            isPatternEnd = true;
        }
    }
    void FireAround2()
    {
        print("�ѷȴٰ� �÷��̾�������� �ι�");

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround2", 0.4f);
        }
        if (curPatternCount == maxPatternCount[patternIndex])
        {
            isPatternEnd = true;
        }
    }


    void EnemyMove(bool isBoss)
    {
        if (isBoss == true && timer >= 4)
        {
            return;
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet") //�÷��̾��Ѿ�
        {
            OnHit(5);
            print(enemyHp);
            if (enemyHp <= 0)
            {
                print("�� ����");
                Destroy(gameObject);
                PlayerScript playerScript = player.GetComponent<PlayerScript>();
                playerScript.score += enemyScore;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player") //�÷��̾�
        {
            Destroy(gameObject);
        }
    }
    public void OnHit(float dmg)
    {
        print("�� ����");
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
