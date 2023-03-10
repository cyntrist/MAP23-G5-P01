using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed;
    float horizontalMove = 0f;
    float crouchMove;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        crouchMove = Input.GetAxis("Agacharse");

        if (Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            jump = true;
        }

        if (crouchMove != 0 && GameManager._marioState != GameManager.MarioStates.PEQUE)
        {
            crouch = true;
        }
        else if (crouchMove == 0)
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

        // Animator (Lo he tenido que meter aqu? porque desde el GameManager no pod?a, mis disculpas, no hay conflictos que haya visto -Cynthia)
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