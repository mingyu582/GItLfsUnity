using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBorder : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet") //�÷��̾��Ѿ�
        {
            Destroy(collision.gameObject);
            print("�Ѿ� �ڵ��ı���");
        }
    }
}
