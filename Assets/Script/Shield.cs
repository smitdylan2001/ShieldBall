using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _shieldGO;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _leftHand;

    private float _handsDistance;
    private Vector3 _handsDistanceVector;
    private Vector3 _handsMiddle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _handsMiddle = (_rightHand.transform.position + _leftHand.transform.position) / 2;
        _handsDistance = Vector3.Distance(_rightHand.transform.position, _leftHand.transform.position);
        _handsDistanceVector = new Vector3(Mathf.Abs(_leftHand.transform.localPosition.x - _rightHand.transform.localPosition.x), Mathf.Abs(_leftHand.transform.localPosition.y - _rightHand.transform.localPosition.y), Mathf.Abs(_leftHand.transform.localPosition.z - _rightHand.transform.localPosition.z));
        _shieldGO.transform.position = _handsMiddle;
        _shieldGO.transform.localScale = new Vector3(_handsDistance, _shieldGO.transform.localScale.y, _handsDistance);
        Debug.LogWarning( Quaternion.Euler(Mathf.Tan(_handsDistanceVector.y / _handsDistanceVector.z) + 90, Mathf.Tan(_handsDistanceVector.z / _handsDistanceVector.x), Mathf.Tan(_handsDistanceVector.y / _handsDistanceVector.x)));
        Debug.Log(_handsDistanceVector.x);

        
        // _shieldGO.transform.rotation = Quaternion.Euler(Mathf.Tan(_handsDistanceVector.y / _handsDistanceVector.z) + 90, Mathf.Tan(_handsDistanceVector.z / _handsDistanceVector.x), Mathf.Tan(_handsDistanceVector.y/_handsDistanceVector.x));
    }
}
