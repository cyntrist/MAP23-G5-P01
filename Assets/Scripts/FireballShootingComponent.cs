using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShootingComponent : MonoBehaviour
{

    #region references
    [SerializeField]
    private GameObject _fireball;
    private Transform _firePoint;
 
    #endregion
  
    void Start()
    {
        _firePoint = this.gameObject.transform.GetChild(2).GetComponent<Transform>();   // cogiendo el spawnpoint

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(_fireball, _firePoint.position, _firePoint.rotation);
            
        }

    }
}
