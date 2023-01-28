using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private Rigidbody2D rb;
    public CharacterController2D controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick"))  //Si se colisiona con Brick
        {
            rb.velocity = Vector2.down * 2;
        }
    }
}
