using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBlood : MonoBehaviour
{
    private float speed = 2f;
    private int hp = 5;
    private int ranItem;

    public GameObject[] ItemPrefabs;
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }
    public void OnHitBlood(int dmg)
    {
        print("백혈구 맞음");
        hp -= dmg;

        if (hp <= 0)
        {
            ranItem = Random.Range(0, 6);
            Instantiate(ItemPrefabs[ranItem], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
