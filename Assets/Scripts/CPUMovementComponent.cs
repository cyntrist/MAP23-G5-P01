using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUMovementComponent : MonoBehaviour
{
    private Transform _cpuTransform;
    private Vector2 _cpuPosition;
    [SerializeField]
    private float _cpuSpeed = 1.0f;
    [SerializeField]
    private float _rayDistance = 0.1f;
    private bool _derecha = false;
    private RaycastHit2D _myRaycastHit;
    private LayerMask _myLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        _cpuTransform = transform;
        _cpuPosition = _cpuTransform.position;
        _myRaycastHit = new RaycastHit2D();
        _myLayerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_derecha == true) _cpuTransform.Translate(Vector2.right * _cpuSpeed * Time.deltaTime);
        else _cpuTransform.Translate(Vector2.left * _cpuSpeed * Time.deltaTime);
        _myRaycastHit = Physics2D.Raycast(_cpuPosition, Vector2.up, _rayDistance, _myLayerMask);
        if (_myRaycastHit.collider != null) Destroy(gameObject);
    }
}