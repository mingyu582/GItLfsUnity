using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBorder : MonoBehaviour
{
    public string borderType;
    private void OnTriggerEnter(Collider collision)
    {
        if (borderType == "down" && collision.gameObject.tag == "Enemy" || borderType == "down" && collision.gameObject.tag == "FallingObject")
        {
            Destroy(collision.gameObject);
            print("Àû ¶Ç´Â ¶³¾îÁö´Â ¹°Ã¼ ÆÄ±«µÊ");
        }
        else if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet") //ÇÃ·¹ÀÌ¾îÃÑ¾Ë
        {
            Destroy(collision.gameObject);
            print("ÃÑ¾Ë ÀÚµ¿ÆÄ±«µÊ");
        }
    }
}
