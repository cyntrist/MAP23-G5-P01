using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    private PowerupController _myPowerupController;
    private MysteryBlockComponent _myMysteryBlock;
    private GameObject _myFlag;
    private Animator _myAnimator;
    private bool _muerto = false;
    private KoopaBehaviour _koopaBehaviour;
    private MovementComponent _myMovementComponent;
    #endregion

    #region Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Seta") && GameManager.MarioState == GameManager.MarioStates.PEQUE) //si tocamos powerup
        {
            _myPowerupController.powerUpGrande(); //obtenemos powerup grande (seta)
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("BrickBase") && GameManager.MarioState >= GameManager.MarioStates.GRANDE) //si tocamos brickbase 
        {
            Destroy(collision.gameObject.transform.parent.gameObject); //se destruye el brick (padre)
        }

        else if (collision.gameObject.CompareTag("MysteryBlock"))
        {
            _myMysteryBlock = collision.gameObject.GetComponent<MysteryBlockComponent>();
            _myMysteryBlock.DropPowerUp();
        }
        else if (collision.gameObject.CompareTag("InvisibleBlock"))
        { //Cuando se detecte colision con el InvisibleBlock, instanciar o activar (?) EmptyBlock que dropee de seguido el 1UP
            collision.gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            collision.gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        else if (collision.gameObject.CompareTag("Flag"))
        {
            gameObject.GetComponent<MovementComponent>().enabled = false;
            _myFlag = collision.gameObject;
            _myFlag.GetComponent<FlagComponent>().EndOfLevel(collision.gameObject);
            _myFlag.GetComponent<BoxCollider2D>().enabled = false;
            _myMovementComponent.GoToCastle();
        }

        else if (collision.gameObject.CompareTag("Cabeza")) //si tocamos enemy
        {
            if (collision.gameObject.transform.parent.GetComponent<KoopaBehaviour>() != null)
            {
                Debug.Log("CAGON");
                _koopaBehaviour = collision.gameObject.transform.parent.GetComponent<KoopaBehaviour>();
                _koopaBehaviour.ShellDrop();
            }
            else
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }

        else if (collision.gameObject.CompareTag("Void"))
        {
            Debug.Log("Tas muerto, Collider: " + collision.gameObject.name);
            _muerto = true;
            // He movido el destroy() al fixed update para que éste se pueda ejecutar y ahí ya se destruye
        }

        else if (collision.gameObject.CompareTag("Coin")) //si tocamos moneda
        {
            GameManager.Instance.AddCoins(1);
            // GameManager.Instance.AddScore(x); dan puntuacion?? -Cynthia
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !GameManager.Instance.i_frames) //si tocamos enemy
        {
            if (GameManager.MarioState >= GameManager.MarioStates.GRANDE)
            {
                Debug.Log(GameManager._marioState);
                _myPowerupController.powerDownGrande();
                GameManager.Instance.i_frames = true;
            }
            else if (GameManager.MarioState == GameManager.MarioStates.PEQUE)
            {
                Debug.Log(GameManager._marioState);
                Debug.Log("Tas muerto, Collider: " + collision.gameObject.name);
                _muerto = true;
                // He movido el destroy() al fixed update para que éste se pueda ejecutar y ahí ya se destruye
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("InvisibleTrigger"))
        {
            other.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("InvisibleTrigger"))
        {
            other.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    #endregion

    // referenciamos este script en el game manager
    private void Awake()
    {
        GameManager.Instance.RegisterCollisionManager(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _myPowerupController = gameObject.GetComponent<PowerupController>();
        _myAnimator = gameObject.GetComponent<Animator>();
        _myMovementComponent = gameObject.GetComponent<MovementComponent>();
    }

    private void FixedUpdate()
    {
        if (_muerto)
        {
            Debug.Log("Muerto");
            GameManager.Instance.OneDown();
            Destroy(gameObject);            // lo ultimisimo porque se destruye este script con él
        }
        _muerto = false;
    }
}

