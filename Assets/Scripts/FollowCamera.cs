using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform _cameraTransform;
    private LayerMask _myLayerMask;
    private Vector2 _direction;
    private CharacterController2D _myCharacterController2D;
    private Transform _playerTransform;
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = transform;
        _playerTransform = _player.transform;
        _myLayerMask = LayerMask.GetMask("Comprobador");
        _myCharacterController2D = _player.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (_myCharacterController2D.m_FacingRight == true)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.left;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_playerTransform != null)
        {
            if (_playerTransform.position.x >= _cameraTransform.position.x && Physics2D.Raycast(_playerTransform.position, _direction, Mathf.Infinity, _myLayerMask))
            {
                _cameraTransform.position = new Vector3(_playerTransform.position.x, _cameraTransform.position.y, 0) + _cameraOffset;
            }
        }
    }
}
