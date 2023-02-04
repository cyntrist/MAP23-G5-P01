using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBlockComponent : MonoBehaviour
{
    [SerializeField] private GameObject _myPowerUp;
    [SerializeField] private Transform _mySpawnPointTransform;
    private bool _spawned;

    public void DropPowerUp()
    {
        if (!_spawned)
        {
            Instantiate(_myPowerUp, _mySpawnPointTransform);
        }
        _spawned = true;
    }

    private void Start()
    {
        _spawned = false;
    }
}
