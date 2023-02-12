using UnityEngine;

public class CPUMovementComponent : MonoBehaviour
{
    private Transform _cpuTransform;
    private Vector2 _cpuPosition;
    [SerializeField]
    private float _cpuSpeed = 0.25f;
    [SerializeField]
    public bool _derecha;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        _cpuTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        //_cpuPosition = _cpuTransform.position;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (_derecha)
        {
            _cpuTransform.Translate(2 * Time.deltaTime * _cpuSpeed, 0, 0);
         
        }
        else
        {
            _cpuTransform.Translate(-2 * Time.deltaTime * _cpuSpeed,0,0);

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tube") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("AntiEnemyFall"))
        {
           
            if (_derecha)
            {
                _derecha = false;
            }
            else
            {
                _derecha = true;

            }
            
            transform.localScale= new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            
        }
    }
}