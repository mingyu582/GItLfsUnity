using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "WhiteBlood")
        {
            WhiteBlood whiteBlood = collision.gameObject.GetComponent<WhiteBlood>();
            whiteBlood.OnHitBlood(2);
        }
    }
}
