using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager
{
    private int _score;
    private int _multiplier;
    private Text _scoreText;

    public void OnInitialize()
    {
        _score = 0;
        EventManager<Shield>.AddListener(EventType.ON_POINTS_UPDATE, AddScore);
    }

    private void AddScore(Shield shield)
	{
        //_score += (int)score;

        //_scoreText.text = _score.ToString();
	}
}
