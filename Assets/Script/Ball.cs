using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    private GameObject _ballGO;
    private Rigidbody _ballRB;
    private float _ballSpeed;
    private float _ballSpeedMultiplier;
    GameObject _shield;
    
    public Ball()
	{
        _ballSpeed = 7;
        _ballGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _ballGO.tag = "Ball";
        _ballGO.GetComponent<SphereCollider>().material = Resources.Load<PhysicMaterial>("Bouncy");
        _ballGO.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _ballRB = _ballGO.AddComponent<Rigidbody>();
        _ballRB.useGravity = false;
    }

    public void OnInitialize(GameObject shield)
    {
        _shield = shield;
        //GetBounced(_ballSpeed, shield.transform.forward);

        //EventManager<Shield>.AddListener(EventType.ON_POINTS_UPDATE, GetBounced);
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
}
