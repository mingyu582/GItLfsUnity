using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;

    public static int highScore;
    public static int curScore;
    [SerializeField]
    private static bool m_bLoad = false;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        ScoreLoad();
    }

    public static DataManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    [SerializeField]
    private static List<ScoreData> m_ScoreArr;
    public static List<ScoreData> ScoreArr { get { return m_ScoreArr; } }
    
    public static void ScoreLoad()
    {
        if (m_bLoad == true)
        {
            return;
        }
        m_ScoreArr= new List<ScoreData>();

        for (int i = 0; i < 5; i++)
        {
            ScoreData NewScore = new ScoreData("", 0);
            m_ScoreArr.Add(NewScore);
        }
        m_bLoad = true;
    }

    public void ScoreInput(string _Name)
    {
        ScoreData CheckData = new ScoreData(_Name, curScore);
        curScore = 0;

        for (int i = 0; i < ScoreArr.Count; i++)
        {
            if (CheckData.Score > ScoreArr[i].Score)
            {
                ScoreData TempScore = ScoreArr[i];
                ScoreArr[i] = CheckData;
                CheckData = TempScore;
            }
        }
    }
}
