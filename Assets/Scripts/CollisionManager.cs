using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    private PowerupController _myPowerupController;
    private MysteryBlockComponent _myMysteryBlock;
    private FlagComponent _myFlag;
    private Animator _myAnimator;
    private bool _muerto = false;
    [SerializeField] private GameObject _goomba;
    #endregion

    #region Methods
    private void OnCollisionStay2D(Collision2D collision)
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
            Debug.Log("Colisión con invisible block");
            //Instantiate(MysteryBlock,transform.parent, Quaternion.identity); 
            //setactive?
            //droppowerup()?
        }

        else if (collision.gameObject.CompareTag("Flag"))
        {
            gameObject.GetComponent<MovementComponent>().enabled = false;
            _myFlag = collision.gameObject.GetComponent<FlagComponent>();
            _myFlag.EndOfLevel(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("Enemy") && !GameManager.Instance.i_frames) //si tocamos enemy
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

        else if (collision.gameObject.CompareTag("Cabeza")) //si tocamos enemy
        {
            Debug.Log("Muere Goomba");
            Destroy(collision.gameObject.transform.parent.gameObject);
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

