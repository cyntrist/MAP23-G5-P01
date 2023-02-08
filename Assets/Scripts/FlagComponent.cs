using UnityEngine;

public class FlagComponent : MonoBehaviour
{
    [SerializeField] GameObject _entireFlag;
    [SerializeField] float _flagSpeed;
    public void EndOfLevel(GameObject entireFlag)
    {
        _entireFlag = entireFlag;
        Debug.Log("Has ganao majo");

        if (_entireFlag.transform.GetChild(0).position.y > _entireFlag.transform.GetChild(3).position.y)
        {
            _entireFlag.transform.GetChild(0).position -= new Vector3(0, _flagSpeed * Time.deltaTime, 0);
        }
    }
}
