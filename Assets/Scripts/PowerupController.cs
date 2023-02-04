using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{

    #region parameters
    [SerializeField]
    private int _escalaPowerup = 2;

    #endregion

    #region methods
    public void powerUpGrande()
    {
        gameObject.transform.localScale *= _escalaPowerup;
        GameManager._marioState = GameManager.MarioStates.GRANDE;
    }

    public void powerDownGrande()
    {
        gameObject.transform.localScale /= _escalaPowerup;
        GameManager._marioState = GameManager.MarioStates.PEQUE;
    }

    public void powerUpFuego()
    {

    }

    private void powerDownFire()
    {
        //_myDisparaFuego.SetActive(false);
    }

    #endregion

    void Start()
    {
        //_myDisparaFuego= _player.GetComponent<DisparaFuego>;
    }
}
