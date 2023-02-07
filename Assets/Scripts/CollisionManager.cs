using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

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
            _myFlag.EndOfLevel(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Enemy") //si tocamos enemy
        {
            if (GameManager.MarioState >= GameManager.MarioStates.GRANDE)
            {
                Debug.Log(GameManager._marioState);
                _myPowerupController.powerDownGrande();
            }
            else if (GameManager.MarioState == GameManager.MarioStates.PEQUE)
            {
                GameManager.Instance.OneDown();
                Debug.Log(GameManager._marioState);
                Destroy(gameObject);
                Debug.Log("Tas muerto");
            }
        }

        else if (collision.gameObject.tag == "Cabeza") //si tocamos enemy
        {
            Debug.Log("Muere Goomba");
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

        else if (collision.gameObject.tag == "Void")
        {
            Destroy(gameObject);
            Debug.Log("Tas muerto");
            GameManager.Instance.OneDown();
        }

        else if (collision.gameObject.tag == "Coin") //si tocamos moneda
        {
            GameManager.Instance.AddCoins(1);
            // GameManager.Instance.AddScore(x); dan puntuacion?? -Cynthia
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPowerupController = gameObject.GetComponent<PowerupController>();
    }
}

