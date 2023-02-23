using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material table;
    public GameObject enemyBulletA;
    public GameObject enemyBulletB;
    private GameObject player;
    private GameObject gameManager;
    public int type;
    public GameObject[] EnemyPrefabs;

    private float enemyBulletSpeed = 5f;
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
    private bool isPatternEnd = false;
    private bool flag = true;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        table = meshRenderer.material;
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
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
        timer2 += Time.deltaTime;
        EnemyMove(isBoss);
        if (isBoss == true)
        {
            if (timer2 > 8f)
            {
                isPatternEnd = true;
                Term();
                timer2 = 0;
            }
            return;
        }
        

        shotDelay += Time.deltaTime;
        Fire();
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
        print("두발씩 플레이어 방향으로");
        //pattern start
        Instantiate(enemyBulletA, transform.position + Vector3.left * 0.4f, Quaternion.identity);
        Instantiate(enemyBulletA, transform.position + Vector3.right * 0.4f, Quaternion.identity);
        //pattern end
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 0.3f);
            isPatternEnd = false;
        }
        if (curPatternCount == maxPatternCount[patternIndex])
        {
            isPatternEnd = true;
        }
    }
    void CreateEnemy()
    {
        print("적소환");
        //pattern start
        Instantiate(EnemyPrefabs[0], transform.position + Vector3.left, Quaternion.identity);
        Instantiate(EnemyPrefabs[1],transform.position + Vector3.right,Quaternion.identity);
        //pattern end
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("CreateEnemy", 1f);
            isPatternEnd = false;
        }
        if (curPatternCount == maxPatternCount[patternIndex])
        {
            isPatternEnd = true;
        }
    }
    void FireArround1()
    {
        print("뿌리기");
        //pattern start
        int roundNumA = 20;
        int roundNumB = 25;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int i = 0; i < roundNum; i++)
        {
            GameObject bullet = Instantiate(enemyBulletB);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody rigid = bullet.GetComponent<Rigidbody>();
            Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / roundNum), Mathf.Sin(Mathf.PI * 2 * i / roundNum));
            rigid.AddForce(dirVec.normalized * 5, ForceMode.Impulse);

            
        }
        //pattern end
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArround1", 0.4f);
            isPatternEnd = false;
        }
        if (curPatternCount == maxPatternCount[patternIndex])
        {
            isPatternEnd = true;
        }
    }
    void FireAround2()
    {
        print("뿌렸다가 플레이어방향으로 두발");
        //pattern start
        if (flag)
        {
            int roundNum = 20;
            for (int i = 0; i < 20; i++)
            {
                GameObject bullet = Instantiate(enemyBulletB);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody rigid = bullet.GetComponent<Rigidbody>();
                Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / roundNum), Mathf.Sin(Mathf.PI * 2 * i / roundNum));
                rigid.AddForce(dirVec.normalized * 5, ForceMode.Impulse);
            }
        }
        else
        {
            Instantiate(enemyBulletA, transform.position + Vector3.left * 0.4f, Quaternion.identity);
            Instantiate(enemyBulletA, transform.position + Vector3.right * 0.4f, Quaternion.identity);
        }
        flag = !flag;
        //pattern end
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround2", 0.4f);
            isPatternEnd = false;
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
    void Fire()
    {
        if (shotDelay >= maxShotDelay && player != null)
        {
            Instantiate(enemyBulletA, transform.position, Quaternion.identity);

            shotDelay = 0f;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet") //플레이어총알
        {
            OnHit(5);
            print(enemyHp);
            if (enemyHp <= 0)
            {
                print("적 죽임");
                
                PlayerScript playerScript = player.GetComponent<PlayerScript>();
                playerScript.score += enemyScore;

                //Boss Kill
                if (type == 3)
                {
                    print("호출됨");
                    GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
                    gameManagerScript.StageEnd();
                }
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player") //플레이어
        {
            if (type == 3)
            {
                return;
            }
            Destroy(gameObject);
        }
    }
    public void OnHit(float dmg)
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
    }
}
