using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    private PowerupController _myPowerupController;
    private MysteryBlockComponent _myMysteryBlock;
    private FlagComponent _myFlag;
    [SerializeField] private GameObject _goomba;
    #endregion

    #region Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Seta" && GameManager.MarioState == GameManager.MarioStates.PEQUE) //si tocamos powerup
        {
            _myPowerupController.powerUpGrande(); //obtenemos powerup grande (seta)
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "BrickBase" && GameManager.MarioState >= GameManager.MarioStates.GRANDE) //si tocamos brickbase 
        {
            Destroy(collision.gameObject.transform.parent.gameObject); //se destruye el brick (padre)
        }

        else if (collision.gameObject.tag == "MysteryBlock")
        {
            _myMysteryBlock = collision.gameObject.GetComponent<MysteryBlockComponent>();
            _myMysteryBlock.DropPowerUp();
        }

        else if (collision.gameObject.tag == "Flag")
        {
            gameObject.GetComponent<MovementComponent>().enabled = false;
            _myFlag = collision.gameObject.GetComponent<FlagComponent>();
            _myFlag.EndOfLevel();
        }

        else if (collision.gameObject.tag == "Enemy" && collision.gameObject.tag != "Cabeza") //si tocamos enemy
        {
            Destroy(_goomba);
        }

        else if (collision.gameObject.tag == "Enemy" && collision.gameObject.tag == "Cabeza") //si tocamos enemy
        {

            if (GameManager.MarioState == GameManager.MarioStates.GRANDE)
            {
                _myPowerupController.powerDownGrande();
            }
            else if (GameManager.MarioState == GameManager.MarioStates.PEQUE)
            {
                Destroy(gameObject);
                Debug.Log("Tas muerto");
            }
        }

        else if (collision.gameObject.tag == "Void")
        {
            Destroy(gameObject);
            Debug.Log("Tas muerto");
        }

        else if (collision.gameObject.tag == "Coin") //si tocamos moneda
        {
            //mandar al game manager para q cambie el score.
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPowerupController = gameObject.GetComponent<PowerupController>();
    }
}

