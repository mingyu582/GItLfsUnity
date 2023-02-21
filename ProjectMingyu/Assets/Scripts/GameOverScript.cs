using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public GameObject inputInitialPanel;
    public GameObject ranking;
    public InputField inputField;
    public Text[] arrText;
    public Text curScoreText;

    private void Update()
    {
        GotoRanking();
    }
    public void GotoRanking()
    {
        if (inputInitialPanel.gameObject.tag == "MainScene")
        {
            SetRanking();
        } 
        else if (inputField.text != "" )
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                name = inputField.text;
                DataManager.Instance.ScoreInput(name);
                inputInitialPanel.SetActive(false);
                ranking.SetActive(true);
                SetRanking();
            }
        }
        else
        {
            SetCurScore();
            inputInitialPanel.SetActive(true);
            ranking.SetActive(false);
        }
    }
    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void SetCurScore()
    {
        curScoreText.text = "YOUR SCORE : " + DataManager.curScore.ToString();
    }
    public void SetRanking()
    {
        for (int i = 0; i < arrText.Length; i++)
        {
            if (DataManager.ScoreArr[i].Name == "")
            {
                arrText[i].text = "¹Ìµî·Ï      000000";
            }
            else
            {
                arrText[i].text = DataManager.ScoreArr[i].Name + "      " + DataManager.ScoreArr[i].Score;
            }

            string Name = DataManager.ScoreArr[i].Name;

            if (Name == "")
            {
                Name = "NONAME";
            }
        }
    }
}
