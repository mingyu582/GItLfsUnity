using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string type;
    private float speed;
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }
}
