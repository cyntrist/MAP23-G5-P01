using UnityEngine;

public class KoopaBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _shellPrefab;
    private RaycastHit2D _myRaycastHit;
    private LayerMask _myLayerMask;
    private float _rayDistance = 0.1f;
    private Transform _cpuTransform;
    private Vector2 _cpuPosition;
    // Start is called before the first frame update
    void Start()
    {
        _cpuTransform = transform;
        _cpuPosition = _cpuTransform.position;
        _myRaycastHit = new RaycastHit2D();
        _myLayerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _myRaycastHit = Physics2D.Raycast(_cpuPosition, Vector2.up, _rayDistance, _myLayerMask);
        if (_myRaycastHit.collider != null) {
            Destroy(gameObject);
            Instantiate(_shellPrefab);
        }
    }
}
