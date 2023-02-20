using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private float speed = 10f;
    private float rotateSpeed = 10f;
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime,Space.World);
        transform.Rotate(new Vector3(32,32,32) * rotateSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
