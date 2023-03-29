using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;
    
    public const string HighScoreKey = "HighScore";
    private float score;

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

// on destroy get and set the highscore
    private void OnDestroy() 
    {
        //if cant find a highscore then its 0
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey,0);
        //if the new score are higher then the highscore add new score as highscore
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
