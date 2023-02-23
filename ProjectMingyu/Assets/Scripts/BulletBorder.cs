using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBorder : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet") //ÇÃ·¹ÀÌ¾îÃÑ¾Ë
        {
            Destroy(collision.gameObject);
            print("ÃÑ¾Ë ÀÚµ¿ÆÄ±«µÊ");
        }
    }
}
