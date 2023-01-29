using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    #region references
    //private DisparaFuego _myDisparaFuego
    private Transform _myTransform;
    #endregion

    #region parameters
    [SerializeField]
    private float _powerupTime;
    [SerializeField]
    private int _escalaPowerup;

    private Vector3 _escala;
    #endregion

    #region methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HaserseGrandesito")
        {
            Debug.Log("aaaaa");
            _myTransform.localScale = _myTransform.localScale * _escalaPowerup;
            Invoke ("powerDown", _powerupTime);
            Debug.Log("eeeee");
        }
        if (collision.gameObject.tag == "disparaFuego")
        {
            //_myDisparaFuego.SetActive(True);
          Invoke("powerDown", _powerupTime);
        }
    }

    private void powerDown()
    {
        _myTransform.localScale = _escala;
        //_myDisparaFuego.SetActive(false);
    }

    #endregion

    void Start()
    {
        _myTransform = GetComponent<Transform>();
        _escala= _myTransform.localScale;
        //_myDisparaFuego= _player.GetComponent<DisparaFuego>;

    }
}
