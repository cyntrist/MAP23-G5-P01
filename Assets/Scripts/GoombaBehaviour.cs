using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaBehaviour : MonoBehaviour
{
    private Transform _goombaTransform;
    private Vector2 _goombaPosition;
    [SerializeField]
    private float _goombaSpeed = 1.0f;
    private bool _derecha = false;
    // Start is called before the first frame update
    void Start()
    {
        _goombaTransform = transform;
        _goombaPosition = _goombaTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_derecha == true) _goombaTransform.Translate(Vector2.right * _goombaSpeed * Time.deltaTime);
        else _goombaTransform.Translate(Vector2.left * _goombaSpeed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            return;
        }
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Muerte");//Llamar a alguna función de otro script para hacer a mario chico o matarlo
        }
        if (col.gameObject.CompareTag("Piesesitos"))
        {
            Destroy(gameObject);
        }
        _derecha = !_derecha;
    }
}