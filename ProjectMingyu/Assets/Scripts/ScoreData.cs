using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour
{
    public int Score;
    public string Name;

    public ScoreData(string name, int score)
    {
        this.Score = score;
        this.Name = name;
    }
}
