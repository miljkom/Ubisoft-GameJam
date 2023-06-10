using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public float smoothSpeed = 0.125f;
    void Start () 
    {
        offset = transform.position - player.transform.position;
    }
    void FixedUpdate ()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
    }

}