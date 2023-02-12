using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishComponent : MonoBehaviour
{
    private float _speed = 4.0f;
    private float _stopDistance = 1.0f;
    private bool _go;
    [SerializeField] Transform _myTargetPoint;

    private Animator animator;

    public void GoToCastle()
    {
        _go = true;
    }

    void Update()
    {
        if (_go && ((_myTargetPoint.position - transform.position).magnitude > _stopDistance))
        {
            if (animator != null)
            {
                animator.ResetTrigger("saltando");
                animator.SetTrigger("caminando");
            }
            var step = _speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, _myTargetPoint.position, step);
        }
    }

    private void Start()
    {
        _go = false;
        animator = GetComponent<Animator>();
    }
}
