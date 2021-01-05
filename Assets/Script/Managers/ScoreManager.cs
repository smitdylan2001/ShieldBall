using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager
{
    private int _score;
    private int _multiplier;
    private TMP_Text _scoreText;

    public ScoreManager() { }

    public void OnInitialize()
    {
        _scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshPro>();
        _score = 0;
        AddScore(0);
        EventManager<float>.AddListener(EventType.ON_POINTS_UPDATE, AddScore);
    }

    private void AddScore(float score)
	{
        Debug.Log("Scoring");
        _score += (int)score;
        
        _scoreText.text = "Score: " + _score.ToString();
	}
}
