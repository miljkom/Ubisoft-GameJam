using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform _player;
    [SerializeField] private float _camOffsetZ = 20f;
    [SerializeField] private float _camOffsetY = 20f;
    [SerializeField] private float _smoothTime = .3f;
    private Vector3 _velocity = Vector3.zero;
    void Update()
    {
        Vector3 cameraPos = new Vector3(_player.position.x, _player.position.y + _camOffsetY, _player.position.z - _camOffsetZ);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref _velocity, _smoothTime);
    }
}