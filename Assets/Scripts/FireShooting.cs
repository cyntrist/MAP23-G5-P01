using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShooting : MonoBehaviour
{
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private Vector2 _force;
    [SerializeField] private Transform _firePoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(_fireBall, _firePoint.transform.parent.gameObject.transform.parent.gameObject.transform);
            _fireBall.transform.position = _firePoint.position;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Z))
        {
            _fireBall.GetComponent<Rigidbody2D>().AddForce(_force);
        }   
    }
}
