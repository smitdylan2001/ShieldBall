using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject _ballGO;
    private Rigidbody _ballRB;
    private float _ballSpeed;
    private float _ballSpeedMultiplier;
    private GameObject _shield;
    private GameObject _playerReference;
    private bool _IsReturning;

    public void OnInitialize(GameObject shield, GameObject player)
    {
        _shield = shield;
        _playerReference = player;
        _ballSpeed = 7;
        _ballRB = _ballGO.GetComponent<Rigidbody>();
        _IsReturning = false;
        //GetBounced(_ballSpeed, shield.transform.forward);
        EventManager.AddListener(EventType.ON_BALL_HIT, DisableIsReturning);
        EventManager<Shield>.AddListener(EventType.ON_POINTS_UPDATE, GetBounced);
    }

    public void onFixedUpdate()
    {
        
    }

    public void GetBounced(Shield shield)
	{
        _ballSpeedMultiplier = shield._handsDistance;
        _ballRB.velocity = shield.transform.up.normalized * _ballSpeed;
	}

    public void GetBounced(Vector3 direction, Vector3 position)
    {
        _ballGO.transform.position = position;
        _ballRB.velocity = direction.normalized * _ballSpeed;
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Target"))
		{
            EventManager<float>.InvokeEvent(EventType.ON_POINTS_UPDATE, _ballSpeedMultiplier);
            collision.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
            StartCoroutine(ReturnBallToPlayer());
		}
	}

    private void DisableIsReturning()
	{
        _IsReturning = false;
	}

    IEnumerator ReturnBallToPlayer()
	{
        _IsReturning = true;
        Vector3 shieldPos = new Vector3(_shield.transform.position.x, _shield.transform.position.y, _shield.transform.position.z - 1);
        _ballRB.velocity = Vector3.zero;

        while (_IsReturning)
		{
            _ballGO.transform.position = Vector3.MoveTowards(_ballGO.transform.position, shieldPos, Time.deltaTime*100);
            yield return null;
		}
	}
}
