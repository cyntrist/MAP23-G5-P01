using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShooting : MonoBehaviour
{
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private float _force;
    [SerializeField] private Transform _firePoint;
    private Rigidbody2D _fireBody;

    private void Start()
    {
            _fireBody = _fireBall.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(_fireBall, _firePoint.transform.parent.gameObject.transform.parent.gameObject.transform);
            _fireBall.transform.position = _firePoint.position;
            _fireBody.AddForce(new Vector2(_force, 0));
        }
    }
}
