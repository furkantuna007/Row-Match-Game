using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelScoreManager : MonoBehaviour {
    public string[] levelNames; 
    public TMP_Text[] scoreTexts; 
    void Start() {
        for (int i = 0; i < levelNames.Length; i++) {
            int score = PlayerPrefs.GetInt(levelNames[i] + "HighScore", 0);
            scoreTexts[i].text = "" + score.ToString();
        }
    }
}
