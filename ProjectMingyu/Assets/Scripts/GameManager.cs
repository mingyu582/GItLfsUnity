using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float fallingObjectTime;
    public GameObject fallingObject;

    private void Update()
    {
        CreateFallingObject();
    }

    void CreateFallingObject()
    {
        
        float maxTime = 1f;
        float spawnX;

        fallingObjectTime += Time.deltaTime;
        if (fallingObjectTime > maxTime)
        {
            Transform tr = fallingObject.transform;
            spawnX = Random.Range(-8f, 8f);
            Instantiate(fallingObject, new Vector3(spawnX, 8, -1), Quaternion.identity);

            fallingObjectTime = 0;
        }
        
    }
    
}
