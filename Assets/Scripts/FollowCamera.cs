using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform _cameraTransform;
    private LayerMask _myLayerMask;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = transform;
        _myLayerMask = LayerMask.GetMask("Comprobador");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_playerTransform.position.x >= 0 && Physics2D.Raycast(_playerTransform.position, Vector2.right, Mathf.Infinity, _myLayerMask))
        {
            _cameraTransform.position = new Vector3(_playerTransform.position.x, 0, 0) + _cameraOffset;
        }
    }
}
