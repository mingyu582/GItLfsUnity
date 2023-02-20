using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    private float curSpawnDelay;
    private float fallingObjectTime;
    public GameObject fallingObject;
    GameObject player;
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    private float nextSpawnDelay;

    public Text scoreText;
    public Slider hpSlider;
    public Text hpText;
    public Slider painSlider;
    public Text painText;

    private void Awake()
    {
        player = GameObject.Find("Player");
        spawnList = new List<Spawn>();
    }
    private void Update()
    {
        curSpawnDelay += Time.deltaTime;

        UpdateUI();
        CreateFallingObject();
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            print(line);

            if (line == null)
            {
                break;
            }
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    void CreateFallingObject()
    {
        
        float maxTime = 1f;
        float spawnX;

        fallingObjectTime += Time.deltaTime;
        if (fallingObjectTime > maxTime)
        {
            spawnX = Random.Range(-8f, 8f);
            Instantiate(fallingObject, new Vector3(spawnX, 8, -1), Quaternion.identity);

            fallingObjectTime = 0;
        }
    }
    public void UpdateHpSlider(int hp)
    {
        hpSlider.value = hp;
        hpText.text = "HP : " + hp + " / 100";
    }
    public void UpdatePainSlider(int pain)
    {
        painSlider.value = pain;
        painText.text = "PAIN : " + pain + " / 100";
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void UpdateUI()
    {
        if (player != null)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            scoreText.text = string.Format("SCORE : " + "{0:0000000}", playerScript.score);
        }
        
    }
    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            default:
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;

        
        if (curSpawnDelay >= nextSpawnDelay)
        {
            Instantiate(enemies[enemyIndex], spawnPoints[enemyPoint].position, enemies[enemyIndex].transform.rotation);

            curSpawnDelay = 0;
        }
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
}
