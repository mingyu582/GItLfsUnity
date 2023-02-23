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
            print("�� �Ǵ� �������� ��ü �ı���");
        }
        else if (collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "EnemyBullet") //�÷��̾��Ѿ�
        {
            Destroy(collision.gameObject);
            print("�Ѿ� �ڵ��ı���");
        }
    }
}
