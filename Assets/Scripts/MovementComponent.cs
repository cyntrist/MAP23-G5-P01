using UnityEngine;

public class MovementComponent : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    private Vector3 _myTargetPoint;
    [SerializeField] GameObject _castle;

    public float runSpeed;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    [SerializeField] private float _stopDistance = 1.0f;


    public void GoToCastle()
    {
        _myTargetPoint = _castle.transform.GetChild(0).gameObject.transform.position;
        if ((_myTargetPoint - transform.position).magnitude > _stopDistance)
        {
            horizontalMove = (_myTargetPoint - transform.position).normalized.magnitude;
            //_myCharacterController.Move(_movementSpeedVector * _movementSpeed * Time.deltaTime);
            controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed, crouch, jump);
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && GameManager._marioState != GameManager.MarioStates.PEQUE)
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            runSpeed += 1;
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            runSpeed -= 1;
        }

        // Animator (Lo he tenido que meter aqu� porque desde el GameManager no pod�a, mis disculpas, no hay conflictos que haya visto -Cynthia)
        if (animator != null)
        {
            if (horizontalMove != 0 && controller.m_Grounded)
            {
                animator.SetTrigger("caminando");
            }
            else
            {
                animator.ResetTrigger("caminando");
            }

            if (!controller.m_Grounded)
            {
                animator.SetTrigger("saltando");
            }
            else
            {
                animator.ResetTrigger("saltando");
            }
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed, crouch, jump);
        jump = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}