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
        if (collision.gameObject.tag != "Ground") //si no se toque el ground
        {

            if (collision.gameObject.tag == "Powerup") //si tocamos powerup
            {
                CurrentPowerState++; //aumentamos estado (mario crece)

                if (CurrentPowerState == PowerupStates.grandote) //si despues de crecer estamos en grandote
                    _myPowerupController.powerUpGrande(); //obtenemos powerup grande (seta)

                else if (CurrentPowerState == PowerupStates.fuego) //si estabamos en fuego
                    _myPowerupController.powerUpFuego(); //obtenemos powerup fuego (flor)
            }

            else if (collision.gameObject.tag == "BrickBase") //si tocamos brickbase
            {
                Destroy(collision.gameObject.transform.parent.gameObject); //se destruye el brick (padre)
            }

            else if (collision.gameObject.tag == "Enemy") //si tocamos enemy
            {
                //mandar al game manager (con (currentstate))
                return; //todos los returns son solo para q no de error.
            }

            else if (collision.gameObject.tag == "Coin") //si tocamos moneda
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

