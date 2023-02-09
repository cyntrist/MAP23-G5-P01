using UnityEngine;

public class CPUMovementComponent : MonoBehaviour
{
    private Transform _cpuTransform;
    private Vector2 _cpuPosition;
    [SerializeField]
    private float _cpuSpeed = 1.0f;
    [SerializeField]
    public bool _derecha = false;
    // Start is called before the first frame update
    void Start()
    {
        _cpuTransform = transform;
        _cpuPosition = _cpuTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_derecha == true) _cpuTransform.Translate(Vector2.right * _cpuSpeed * Time.deltaTime);
        else _cpuTransform.Translate(Vector2.left * _cpuSpeed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision collision) { 
        _derecha = !_derecha;
    }
}