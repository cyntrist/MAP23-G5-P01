using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region parameters
    public enum PowerupStates { chikito, grandote, fuego }
    public PowerupStates CurrentPowerState = PowerupStates.chikito;
    #endregion

    #region references
    private PowerupController _myPowerupController;
    #endregion

    #region Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground") //si no se toca el ground
        {

            if (collision.gameObject.tag == "Powerup") //si tocamos powerup
            {
                CurrentPowerState++; //aumentamos estado (mario crece)

                if (CurrentPowerState == PowerupStates.grandote)
                {
                    _myPowerupController.powerUpGrande(); //obtenemos powerup grande (seta)
                } //despues de crecer estamos en grandote       
                else if (CurrentPowerState == PowerupStates.fuego)
                {
                    _myPowerupController.powerUpFuego(); //obtenemos powerup fuego (flor)
                } //si estabamos en fuego 
            }

            else if (collision.gameObject.tag == "BrickBase" && CurrentPowerState >= PowerupStates.grandote) //si tocamos brickbase
            {
                Destroy(collision.gameObject.transform.parent.gameObject); //se destruye el brick (padre)
            } 

            else if (collision.gameObject.tag == "Enemy") //si tocamos enemy
            {
                //mandar al game manager (con (currentstate))
            }

            else if (collision.gameObject.tag == "Coin") //si tocamos moneda
            {
                //mandar al game manager para q cambie el score.

            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPowerupController = GetComponent<PowerupController>();

    }
}

