using UnityEngine;

public class FlagComponent : MonoBehaviour
{
    private GameObject _Flag;
    private bool _flagDown;
    [SerializeField] float _flagSpeed;

    public void EndOfLevel(GameObject Flag)
    {
        Debug.Log("Has ganao majo");
        _Flag = Flag.transform.parent.gameObject;
        _flagDown = true;
    }

    /*
    private void Update()
    {
        if (_flagDown && (_Flag.transform.GetChild(0).position.y > _Flag.transform.GetChild(3).position.y))
        {
            _Flag.transform.GetChild(0).position -= new Vector3(0, _flagSpeed * Time.deltaTime, 0);
        }
    }
    */

    private void Start()
    {
        _flagDown = true;
    }
}
