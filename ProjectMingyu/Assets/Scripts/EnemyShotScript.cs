using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour
{
    private GameObject player;
    private float speed = 1f;
    Vector3 dirVec;

    private void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            dirVec = player.transform.position - transform.position;
        }
    }
    private void Update()
    {
        EnemyShot();
    }

    void EnemyShot()
    {
        transform.Translate(dirVec * speed * Time.deltaTime);
    }
}
