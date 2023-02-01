using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaBehaviour : MonoBehaviour
{
    private Transform _goombaTransform;
    private Vector2 _goombaPosition;
    [SerializeField]
    private float _goombaSpeed = 1.0f;
    [SerializeField]
    private float _rayDistance = 0.1f;
    private bool _derecha = false;
    private RaycastHit2D _myRaycastHit;
    private LayerMask _myLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        _goombaTransform = transform;
        _goombaPosition = _goombaTransform.position;
        _myRaycastHit = new RaycastHit2D();
        _myLayerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_derecha == true) _goombaTransform.Translate(Vector2.right * _goombaSpeed * Time.deltaTime);
        else _goombaTransform.Translate(Vector2.left * _goombaSpeed * Time.deltaTime);
        _myRaycastHit = Physics2D.Raycast(_goombaPosition, Vector2.up, _rayDistance, _myLayerMask);
        if (_myRaycastHit.collider != null) Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        /*if (col.gameObject.CompareTag("Ground")) innecesario
        {
            return;
        }*/
        _derecha = !_derecha;
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Muerte");//Llamar a alguna función de otro script para hacer a mario chico o matarlo
        }
        if (col.gameObject.CompareTag("Piesesitos"))
        {
            Debug.Log("Muere Goomba");
            Destroy(gameObject);
        }

    }
}