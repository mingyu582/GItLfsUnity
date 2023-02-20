using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    float upPosY = 0f;
    float downPosY = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;
    void Start()
    {
        yScreenHalfSize = Camera.main.orthographicSize;          //4.5

        downPosY = -(yScreenHalfSize * 2);
        upPosY = yScreenHalfSize * 2 * backgrounds.Length/3*2; //9 * 3 = 27
        //print("@"+upPosY);
        //print(downPosY); // -9
        //print(upPosY);   // 27
    }
    void Update()
    {
        BackgroundScroll();
    }

    void BackgroundScroll()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(0, -speed, 0) * Time.deltaTime;

            if (backgrounds[i].transform.position.y < downPosY)
            {
                Vector3 nextPos = backgrounds[i].position;
                //nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                nextPos = new Vector3(nextPos.x, upPosY, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}