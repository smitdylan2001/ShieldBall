using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    [SerializeField] Camera mainCam;

    private Vector3 beginPos;
    private Vector3 endPos;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        beginPos = transform.position;
        endPos = mainCam.transform.position;
        endPos.z += 3;
        startTime = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        SlerpBall(beginPos, endPos, startTime, 3.0f);
    }

    private void SlerpBall(Vector3 beginPoint, Vector3 endPoint, float startTime, float speed)
    {

        // The point begin the beginning point the ball starts and the end point it has to travel towards
        Vector3 center = (beginPoint + endPoint) * 0.5f;

        // The direction of the curve. How higher the numbers, the flatter the curve.
        center -= new Vector3(0, 20, 0);

        // The distance between de center and the begin/end point
        Vector3 beginToCenter = beginPoint - center;
        Vector3 endToCenter = endPoint - center;

        // The time that it takes for the ball to reach it's destination
        float timePast = (Time.time - startTime) / speed;

        // Slerping the position from beginpoint to end point
        transform.position = Vector3.Slerp(beginToCenter, endToCenter, timePast);
        transform.position += center;

    }

}
