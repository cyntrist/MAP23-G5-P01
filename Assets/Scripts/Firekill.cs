using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firekill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Cabeza") || || collision.gameObject.CompareTag("Cuerpo"))
        {
            Destroy(collision.gameObject);
        }
    }
}
