using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region parameters
    public enum PowerupStates { chikito, grandote, fuego }
    public PowerupStates CurrentPowerState = PowerupStates.chikito;
    #endregion

    #region Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground")
        {

            if (collision.gameObject.tag == "Powerup")
            {
                CurrentPowerState++;

                if (CurrentPowerState == PowerupStates.grandote)
                    _myPowerupController.powerUpGrande();

                else if (CurrentPowerState == PowerupStates.fuego)
                    _myPowerupController.powerUpFuego();
            }

            else if (collision.gameObject.tag == "BrickBase")
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }

            else if (collision.gameObject.tag == "Enemy")
            {
                //mandar al game manager (con (currentstate))
                return; //todos los returns son solo para q no de error.
            }

            else if (collision.gameObject.tag == "Coin")
            {
                //mandar al game manager para q cambie el score.
                return;
            }
        }
    }
    #endregion

    #region references
    private PowerupController _myPowerupController;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _myPowerupController = GetComponent<PowerupController>();

    }
}

