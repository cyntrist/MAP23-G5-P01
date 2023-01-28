using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    #region references
    //todo esto comentado xq no hay todavia nada.
    // private HaserseGrandeComponent _myHaserseGrande;
    //private DisparaFuego _myDisparaFuego
    [SerizalizeField]
    private GameObject _player;
    #endregion
    #region methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "haserseGrandesito")
        {
           //_myHaserseGrande.Hasersegrande();
        }
        if (collision.gameObject.tag == "disparaFuego")
        {
            //_myDisparaFuego.DisparaFuego();
        }
    }

    #endregion

    void Start()
    {
        //_myHaserseGrande = _player.GetComponent<HaserseGrande>;
        //_myDisparaFuego= _player.GetComponent<DisparaFuego>;

    }
}
