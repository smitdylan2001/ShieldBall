using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private Shield _shieldScript;
    private Ball _ballScript;
    private GameObject _player;
    GameObject _shield;
    [SerializeField] GameObject _startPoint;

    void Awake()
    {
        _shieldScript = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _ballScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        _scoreManager = new ScoreManager();

        _scoreManager.OnInitialize();
        _shield = _shieldScript.OnInitialize(_ballScript);
        _ballScript.OnInitialize(_shield, _player);
    }

    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        _ballScript.onFixedUpdate();
	}

    public void GetStarted()
	{
        _ballScript.GetBounced(_startPoint.transform.forward, _startPoint.transform.position);
	}
}
