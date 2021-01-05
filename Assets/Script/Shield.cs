using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _shieldGO;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _leftHand;

    private Ball _ballScript;

    public float _handsDistance { get; private set; }
    private Vector3 _handsDistanceVector;
    private Vector3 _handsMiddle;

    //TODO: Redo with proper angles for direction
    public GameObject OnInitialize(Ball ball)
    {
        _ballScript = ball;

        //_shieldGO = Resources.Load<GameObject>("Shield.prefab"); //TODO: Implement prefab in resources

        //_shieldGO.AddComponent<this>();
        return _shieldGO;
    }

	private void Update()
	{
        _shieldGO.transform.position = _leftHand.transform.position;
        _shieldGO.transform.LookAt(_rightHand.transform);
        float value = (_leftHand.transform.rotation.eulerAngles.x + _rightHand.transform.rotation.eulerAngles.x) / 2;
        //Debug.Log(value);
        Quaternion q = Quaternion.Euler(_shieldGO.transform.rotation.eulerAngles.x + 90 - value, _shieldGO.transform.rotation.eulerAngles.y + 90, _shieldGO.transform.rotation.eulerAngles.z);
        _shieldGO.transform.rotation = q;

        _handsMiddle = (_rightHand.transform.position + _leftHand.transform.position) / 2;
        _handsDistance = Vector3.Distance(_rightHand.transform.position, _leftHand.transform.position);
        _handsDistanceVector = new Vector3(Mathf.Abs(_leftHand.transform.localPosition.x - _rightHand.transform.localPosition.x), 
                                                Mathf.Abs(_leftHand.transform.localPosition.y - _rightHand.transform.localPosition.y), 
                                                Mathf.Abs(_leftHand.transform.localPosition.z - _rightHand.transform.localPosition.z));
        _shieldGO.transform.position = _handsMiddle;
        _shieldGO.transform.localScale = new Vector3(_handsDistance, _shieldGO.transform.localScale.y, _handsDistance);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Ball"))
        {
            //_ballScript.GetBounced(_handsDistance, _shieldGO.transform.forward);
            EventManager<Shield>.InvokeEvent(EventType.ON_BALL_HIT, this);
            EventManager.InvokeEvent(EventType.ON_BALL_HIT);
        }
	}
}
