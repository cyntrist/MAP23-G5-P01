using UnityEngine;

public class KoopaBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _shellPrefab;

    // Update is called once per frame

    public void ShellDrop()
    {
        Instantiate(_shellPrefab, transform.parent.gameObject.transform);
        _shellPrefab.transform.position = transform.position;
        Destroy(gameObject);
    }
}
