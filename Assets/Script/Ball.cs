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
    private Vector3 _beginPos;
    private Vector3 _endPos;
    private float _startTime;
    private Vector3 _velocityCache;

    public void OnInitialize(GameObject shield, GameObject player)
    {
        _shield = shield;
        _playerReference = player;
        _ballSpeed = 7;
        _ballRB = _ballGO.GetComponent<Rigidbody>();
        _IsReturning = false;
        EventManager.AddListener(EventType.ON_BALL_HIT, DisableIsReturning);
        _beginPos = transform.position;
        _endPos.z += 3;
        _startTime = Time.time;
    }

    public void onFixedUpdate()
    {
        
    }

    public void GetBounced(Shield shield)
	{
        _ballSpeedMultiplier = shield._handsDistance;
        _ballRB.velocity = shield.transform.up.normalized * GameManager.BallSpeed;
        DisableIsReturning();

    }

    public void GetBounced(Vector3 direction, Vector3 position)
    {
        _ballGO.transform.position = position;
        _ballRB.velocity = direction.normalized * GameManager.BallSpeed;
        DisableIsReturning();
    }

    public void GetBounced(Vector3 direction)
    {
        _ballRB.velocity = direction.normalized * GameManager.BallSpeed;
        DisableIsReturning();
    }

    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Target"))
		{

            EventManager<float>.InvokeEvent(EventType.ON_POINTS_UPDATE, GameManager.BallSpeed);
            collision.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
            Vector3 v = _shield.transform.position; // + (_shield.transform.up / 5);
            StartCoroutine( ReturnBallToPlayer(v));
		}
	}

    private void DisableIsReturning()
	{
        _IsReturning = false;
	}

    IEnumerator ReturnBallToPlayer(Vector3 v3)
	{
        _IsReturning = true;
        _velocityCache = _ballRB.velocity;
        _ballRB.velocity = Vector3.zero;
        _startTime = Time.time;
        
        while (_IsReturning) //FIXME without while or with extra check
        {
            SlerpBall(_ballGO.transform.position, v3, _startTime, 10); //TODO: FIXME
            yield return null;
        }
    }

    private void SlerpBall(Vector3 beginPoint, Vector3 endPoint, float startTime, float time)
    {
        
        // The point begin the beginning point the ball starts and the end point it has to travel towards
        Vector3 center = (beginPoint + endPoint) * 0.5f;

        // The direction of the curve. How higher the numbers, the flatter the curve.
        center -= new Vector3(0, 20, 0);

        // The distance between de center and the begin/end point
        Vector3 beginToCenter = beginPoint - center;
        Vector3 endToCenter = endPoint - center;

        // The time that it takes for the ball to reach it's destination
        float timePast = (Time.time - startTime) / time;

        // Slerping the position from beginpoint to end point
        transform.position = Vector3.Slerp(beginToCenter, endToCenter, timePast);
        transform.position += center;
    }
}
