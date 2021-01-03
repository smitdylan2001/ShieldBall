using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private Shield _shieldScript;
    private Ball _ballScript;
    GameObject shield;
    [SerializeField] GameObject _startPoint;

    void Awake()
    {
        _shieldScript = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        _ballScript = new Ball();
        _scoreManager = new ScoreManager();

        _scoreManager.OnInitialize();
        shield = _shieldScript.OnInitialize(_ballScript);
        _ballScript.OnInitialize(shield);
    }

    // Update is called once per frame
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
