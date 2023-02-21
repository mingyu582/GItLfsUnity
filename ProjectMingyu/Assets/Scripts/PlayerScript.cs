using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public AudioClip clip;
    MeshRenderer meshRenderer;
    public GameObject playerBulletPrefab;
    public Transform shotPoint;
    public GameObject manager;

    private float horizontal;
    private float vertical;
    private float speed = 10f;

    public float shotDelay;
    public float maxShotDelay = 0.2f;

    private Vector2 playerSize;
    private Vector3 min,max;
    //»ó y
    //ÇÏ -y
    //ÁÂ -x
    //¿ì x
    public int score;
    public int hp = 100;
    public int pain = 100;

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
        if (Input.GetKey(KeyCode.J) && shotDelay >= maxShotDelay)
        {
            SoundManager.instance.SFXPlay("PlayerShot",clip);
            Instantiate(playerBulletPrefab, shotPoint.position, Quaternion.Euler(-90.0f, 0, 0));

            shotDelay = 0f;
        }
        print(meshRenderer.material.color + ".....");
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
        GameManager managerScript = manager.GetComponent<GameManager>();
        print("Àû ÃÑ¾Ë¿¡ ¸Â°Å³ª ÀÚÆøÇÔ");

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            hp -= 15;
            managerScript.UpdateHpSlider(hp);
            if (hp <= 0)
            {
                print("ºñÇà±â ÆÄ±«µÊ");
                print(score);
                DataManager.curScore = score;
                managerScript.GameOver(score);
                Destroy(gameObject);
            }
            else
            {
                //ÇÃ·¹ÀÌ¾î ±ôºý°Å¸² ¹«Àû½Ã°£ 3ÃÊ

                meshRenderer.material = Resources.Load<Material>("Materials/Damaged");
                Invoke("ReturnMaterial", 0.1f);
            }
            Destroy(collision.gameObject);
        }
        
        
    }

    void ReturnMaterial()
    {
        meshRenderer.material = Resources.Load<Material>("Materials/StarSparrow_White");
    }
}
