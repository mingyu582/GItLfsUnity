using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    //static string nextScene;





    /*public static void LoadScene(string sceneName)
    {
        nextScene= sceneName;
        SceneManager.LoadScene(nextScene);
    }*/
    public Slider progressBar;

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("GameScene");
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if (progressBar.value <= 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value,1f,Time.deltaTime);
            }
            else
            {
                timer += Time.deltaTime;
                progressBar.value = Mathf.Lerp(0.9f, 1f, timer);

                
                if (progressBar.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
