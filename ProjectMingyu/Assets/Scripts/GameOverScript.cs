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

    private void Update()
    {
        GotoRanking();
    }
    public void GotoRanking()
    {
        
        if (inputField.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inputInitialPanel.SetActive(false);
                ranking.SetActive(true);
            }
        }
        else
        {
            inputInitialPanel.SetActive(true);
            ranking.SetActive(false);
        }
    }
    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
