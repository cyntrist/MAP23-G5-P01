using UnityEngine;

public class MovementComponent : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("CrucetaX") * runSpeed;

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
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

        // Animator (Lo he tenido que meter aquí porque desde el GameManager no podía, mis disculpas, no hay conflictos que haya visto -Cynthia)
        if (animator!= null)
        {
            if (horizontalMove != 0)
            {
                animator.SetTrigger("caminando");
            }
            else
            {
                animator.ResetTrigger("caminando");
            }

            if (jump)
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