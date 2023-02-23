using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    private int stage = 1;

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

    public GameObject pausePanel;
    public Text BGMValueText;
    public Text SFXValueText;

    public GameObject redBlood;
    public GameObject whiteBlood;
    private float bloodSpawnTime;

    public GameObject startStage;
    public GameObject stageClear;
    public Text StageText;
    private bool stageFlag = false;
    private bool clearFlag = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
        spawnList = new List<Spawn>();
        StageStart();
    }
    private void Start()
    {
        pausePanel.SetActive(false);
    }
    private void Update()
    {
        bloodSpawnTime += Time.deltaTime;
        curSpawnDelay += Time.deltaTime;

        UpdateUI();
        CreateFallingObject();

        //spawn blood
        SpawnBlood();

        //spawn enemy
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        //pause panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("sa;dlk");
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnClickTitle()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickBack()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }


    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage "+ stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            print("line: "+line);

            if (line == null)
            {
                break;
            }
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
            print(spawnData.delay + " " + spawnData.type + " " + spawnData.point);
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    public void StageStart()
    {
        //UI Load
        StageStartUI();
        //ReadSpawnFile
        ReadSpawnFile();
    }
    public void StageEnd()
    {
        //Clear

        //stage Áõ°¡
        stage++;
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
    public void GameOver(int score)
    {
        DataManager.curScore= score;
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
            case "0":
                enemyIndex = 0;
                break;
            case "1":
                enemyIndex = 1;
                break;
            case "2":
                enemyIndex = 2;
                break;
            case "3":
                enemyIndex = 3;
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
    void SpawnBlood()
    {
        int bloodType = Random.Range(0, 2);
        int randomPos = Random.Range(0, 5);
        float randomSpawnTime = Random.Range(10.0f, 15.0f);
        if (bloodSpawnTime < randomSpawnTime)
        {
            return;
        }
        if (bloodType == 0)
        {
            Instantiate(redBlood, spawnPoints[randomPos].position, Quaternion.identity);
        }
        else
        {
            Instantiate(whiteBlood, spawnPoints[randomPos].position, Quaternion.identity);
        }
    }
    void StageStartUI()
    {
        stageFlag =!stageFlag;

        if (stageFlag == true)
        {
            startStage.SetActive(true);
            StageText.text = "STAGE " + stage; 
        }
        else
        {
            startStage.SetActive(false);
        }
    }
    void StageEndUI()
    {
        clearFlag = !clearFlag;

        if (clearFlag == true)
        {
            stageClear.SetActive(true);
        }
        else
        {
            stageClear.SetActive(false);
        }
    }
}
