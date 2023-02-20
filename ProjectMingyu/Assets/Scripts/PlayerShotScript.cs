using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotScript : MonoBehaviour
{
    private float speed = 5f;
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime,Space.World);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
