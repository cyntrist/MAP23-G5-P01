using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    #region parameters
    #endregion

    #region references
    [SerializeField]
    private FXComponent _FXComponent;
    public OSTComponent _OSTComponent;

    private PowerupController _myPowerupController;
    private MysteryBlockComponent _myMysteryBlock;
    private GameObject _myFlag;
    private Animator _myAnimator;
    private bool _muerto = false;
    private bool _koopaMuerto = false;
    private bool _coinPickup = false;
    private KoopaBehaviour _koopaBehaviour;
    private MovementComponent _myMovementComponent;
    private FinishComponent _myFinishComponent;
    #endregion

    #region Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Seta") && GameManager.MarioState == GameManager.MarioStates.PEQUE) //si tocamos Seta y somos chikitos
        {
            _FXComponent.PlaySound(0);
            _myPowerupController.powerUpGrande(); // A mi me funciona, si sigue dando problemas, hacer aquí un bool = true y llamada al método en fixed
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("1UP")) //si tocamos 1UP
        {
            _FXComponent.PlaySound(0);
            GameManager.Instance.OneUp(); // A mi me funciona, si sigue dando problemas, hacer aquí un bool = true y llamada al método en fixed
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("FireFlower") && GameManager.MarioState == GameManager.MarioStates.GRANDE) //si tocamos flor y somos grandes
        {
            _myPowerupController.powerUpFuego(); // A mi me funciona, si sigue dando problemas, hacer aquí un bool = true y llamada al método en fixed
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
            _FXComponent.PlaySound(1);
            collision.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true; //se activa el render del empty block
                                                                                                                 //al colisionar con mystery block
        }

        else if (collision.gameObject.CompareTag("Flag"))
        {
            _FXComponent.PlaySound(4);
            _myMovementComponent.enabled = false;
            _myFlag = collision.gameObject;
            _myFlag.GetComponent<FlagComponent>().EndOfLevel(collision.gameObject);
            GameObject _myOSTObject = GameObject.Find("OSTComponent");
            _OSTComponent = _myOSTObject.GetComponent<OSTComponent>();
            _OSTComponent.PlaySound(3);
        }

        else if (collision.gameObject.CompareTag("LastHardBlock"))
        {
            _myFlag.GetComponent<FlagComponent>().enabled = false;
            collision.gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _myFinishComponent.GoToCastle();
        }

        else if (collision.gameObject.CompareTag("Cabeza")) //si tocamos enemy
        {
            _FXComponent.PlaySound(7);
            if (collision.gameObject.transform.parent.GetComponent<KoopaBehaviour>() != null)
            {
                Debug.Log("CAGON"); // a veces crea 2 shells
                _koopaBehaviour = collision.gameObject.transform.parent.GetComponent<KoopaBehaviour>();
                _koopaMuerto = true;
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
        }

        else if (collision.gameObject.CompareTag("Coin")) //si tocamos moneda
        {
            _coinPickup = true; // Esto sí que estaba roto, arreglado de la misma manera (a veces suman 2 xd)
            Debug.Log("+1 MONEDA");
            // GameManager.Instance.AddScore(x); dan puntuacion?? -Cynthia
            Debug.Log("sondio moneda");
            _FXComponent.PlaySound(3);
            Debug.Log("se acabó sonido moneda");
            Destroy(collision.gameObject);
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
                Debug.Log("Sonido bajar vida");
                _FXComponent.PlaySound(2);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InvisibleBlock"))
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true; //enables el Renderer al colisionar
            collision.gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            collision.gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            _myMysteryBlock = collision.gameObject.GetComponent<MysteryBlockComponent>();
            _myMysteryBlock.DropPowerUp();
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
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    #endregion

    // referenciamos este script en el game manager
    private void Awake()
    {
        GameManager.Instance.RegisterCollisionManager(this); // Da error al ejecutar
    }

    // Start is called before the first frame update
    void Start()
    {
        _myPowerupController = gameObject.GetComponent<PowerupController>();
        _myAnimator = gameObject.GetComponent<Animator>();
        _myMovementComponent = gameObject.GetComponent<MovementComponent>();
        _myFinishComponent = GetComponent<FinishComponent>();
    }

    private void FixedUpdate()
    {
        if (_coinPickup)
        {
            GameManager.Instance.AddCoins(1);
        }

        if (_koopaMuerto && _koopaBehaviour != null)
        {
            _koopaBehaviour.ShellDrop();
        }

        if (_muerto)
        {
            Debug.Log("Muerto");
            _FXComponent.PlaySound(11);
            GameManager.Instance.OneDown();
            Destroy(gameObject);            // lo ultimisimo porque se destruye este script con él
        }
        _muerto = false;
        _koopaMuerto = false;
        _coinPickup = false;
    }

    
    
}

