using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public static bool _death;
    GameObject _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _death = false;
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        /*  if (death)  //animacion de muerte
          {
              Animator.SetTrigger("Death");
          } */

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Debug.Log("muerto");
        }
    }



}
