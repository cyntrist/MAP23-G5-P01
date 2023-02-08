using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public float speed = 15;
    private Rigidbody2D _myRigidBody;

    void Start()
    {

        _myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _myRigidBody.velocity = transform.right * speed;
    }
}
