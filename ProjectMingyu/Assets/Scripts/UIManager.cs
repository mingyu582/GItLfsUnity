using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject helpPanelGroup;
    public GameObject helpPanel;
    public GameObject title;
    public GameObject contentsPanel;
    public GameObject virusPanel;
    public GameObject controlPanelGroup;
    public GameObject itemPanelGroup;
    public GameObject contents;


    public void OnClickStartButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void OnClickHowToPlayer()
    {
        helpPanelGroup.SetActive(true);
        title.SetActive(false);
        helpPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        helpPanel.SetActive(false);
        title.SetActive(true);
    }
    public void BackToHelpMenu()
    {
        helpPanel.SetActive(true);
        contentsPanel.SetActive(false);
    }
    public void OnClickVirusButton()
    {
        helpPanel.SetActive(false);
        contents.SetActive(true);

        virusPanel.SetActive(true);
        controlPanelGroup.SetActive(false);
        itemPanelGroup.SetActive(false);
    }
    public void OnClickControlButton()
    {
        helpPanel.SetActive(false);
        contents.SetActive(true);

        virusPanel.SetActive(false);
        controlPanelGroup.SetActive(true);
        itemPanelGroup.SetActive(false);
    }
    public void OnClickItemButton()
    {
        helpPanel.SetActive(false);
        contents.SetActive(true);

        virusPanel.SetActive(false);
        controlPanelGroup.SetActive(false);
        itemPanelGroup.SetActive(true);
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
