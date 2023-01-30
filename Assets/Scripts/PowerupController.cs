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
    private int _escalaPowerup;

    private Vector3 _escala;
    #endregion

    #region methods
    public void powerUpGrande()
    {
        _myTransform.localScale = _myTransform.localScale * _escalaPowerup;
    }

    public void powerDownGrande()
    {
        _myTransform.localScale = _escala;
    }

    public void powerUpFuego()
    {
        //_myDisparaFuego.SetActive(true)
        return;
    }

    private void powerDownFire()
    {
        //_myDisparaFuego.SetActive(false);
        return; //todos los returns son para que no de error, hay que quitarlos cuando se acabe el código.
    }

    #endregion

    void Start()
    {
        _myTransform = GetComponent<Transform>();
        _escala= _myTransform.localScale;
        //_myDisparaFuego= _player.GetComponent<DisparaFuego>;

    }
}
