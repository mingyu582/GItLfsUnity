using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject playerBulletPrefab;
    public Transform shotPoint;

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
    public int life;

    private void Start()
    {
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
            Instantiate(playerBulletPrefab, shotPoint.position, Quaternion.Euler(-90.0f, 0, 0));

            shotDelay = 0f;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        print("Àû ÃÑ¾Ë¿¡ ¸Â°Å³ª ÀÚÆøÇÔ");

        Destroy(other.gameObject);
        Destroy(gameObject);
        
    }
}
