using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotScript : MonoBehaviour
{
    private GameObject player;
    private float speed = 1f;
    Vector3 dirVec;
    public string bulletType;

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
        if (bulletType == "A")
        {
            EnemyShotA();
        } else if (bulletType == "B")
        {
            EnemyShotB();
        }
        
    }

    void EnemyShotA()
    {
        transform.Translate(dirVec * speed * Time.deltaTime);
    }
    void EnemyShotB()
    {
        
    }
}
