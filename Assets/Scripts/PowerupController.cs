using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{

    #region parameters
    [SerializeField]
    private int _escalaPowerup;

    #endregion

    #region methods
    public void powerUpGrande()
    {
        gameObject.transform.localScale *= _escalaPowerup;
    }

    public void powerDownGrande()
    {
        gameObject.transform.localScale = Vector2.one;
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
        //_myDisparaFuego= _player.GetComponent<DisparaFuego>;
    }
}
