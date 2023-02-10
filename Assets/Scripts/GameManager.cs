using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { START, INTRO, GAME, GAMEOVER };    // Estados del juego
    public enum MarioStates { PEQUE, GRANDE, FUEGO, ESTRELLA }; // Estados de Mario

    #region references
    private UIManager _UIManager;           // Referencia al UIManager
    [SerializeField] GameObject _level;     // Referencia al prefab del nivel 1-1
    private CollisionManager _collisionManager; //referencia al collision manager
    #endregion

    #region properties
    public bool i_frames = false;
    private int tiempo_inmortal;
    private const int TIEMPOJUEGO = 300;
    private const int TIEMPOINTRO = 3;
    private static GameManager _instance;       // Instancia privada del singleton
    private GameStates _currentState;           // Estado actual del juego
    private GameStates _nextState;              // Siguiente estado del juego
    public static MarioStates _marioState;      // Estado actual de Mario
    private float _remainingTime = TIEMPOJUEGO; // Segundos que dura el nivel
    private float _introTime = TIEMPOINTRO;     // Segundos que dura la pantalla en negro al cargar el nivel (INTRO state)
    private int _lives = 3;                     // Vidas de Mario
    private int _score;                         // Puntuación total en el nivel
    private int _coins;                         // Monedas totales en el nivel
    private GameObject _levelInstance;          // Instancia del nivel (solo es para el Unload() porque si no me daba error :P)
    public static GameManager Instance { get { return _instance; } } // MÉTODOS GETTER
    public GameStates CurrentState { get { return _currentState; } }
    public static MarioStates MarioState { get { return _marioState; } }
    public int Lives { get { return _lives; } }
    public int Score { get { return _score; } }
    public int Coins { get { return _coins; } }
    #endregion

    #region methods
    // BLOQUE DE REGISTROS DE REFERENCIAS 
    public void RegisterUIManager(UIManager uiManager)
    {
        _UIManager = uiManager;
    }

    // referencia al colision
    public void RegisterCollisionManager(CollisionManager collisionManager)
    {
        _collisionManager = collisionManager;
    }

    // BLOQUE DE JUEGO 
    public void OneUp() // champiñon verde / 100 monedas
    {
        _lives++;
        Debug.Log("Vida++");
    }
    public void OneDown() // rip 
    {
        _lives--;
        if (_lives > 0)
        {
            _nextState = GameStates.INTRO; // Restart
        }
        else
        {
            _nextState = GameStates.GAMEOVER;
        }
    }
    public void AddScore(int points) // Solo add porque nunca se restan
    {
        _score += points;
    }
    public void AddCoins(int coins) // Solo add porque creo que nunca resta
    {
        _coins += coins;
    }
    public void RequestMarioChange(MarioStates state) // Para cambiar directamente a uno
    {
        _marioState = state;
    }
    public void AddMarioState() // Para powerups
    {
        _marioState++;
    }
    public void SubMarioState() // Cuando es dañado
    {
        _marioState--;
    }

    // BLOQUE DE MÁQUINA DE ESTADOS DEL JUEGO
    public void EnterState(GameStates newState)
    {
        switch (newState) // Diferentes comportamientos según estado al que se entra
        { // En sí, solo cambia el grupo de UI por cada estado y en GAME carga el nivel
            case GameStates.START:                       //     *MENÚ INICIAL*
                _UIManager.SetMenu(GameStates.START);    // Activa menú inicial
                break;
            case GameStates.INTRO:                       //     *INTRO* (Pantalla en negro con las vidas antes de cargar el lvl)
                _introTime = TIEMPOINTRO;                // Reinicia el timer
                if (_levelInstance != null)
                {
                    UnloadLevel();                       // Si el nivel existe lo destruye
                }
                _UIManager.SetLives(_lives);             // Inicializa valores de vida en la UI
                _UIManager.SetMenu(GameStates.INTRO);    // Activa menú intro
                break;
            case GameStates.GAME:                        //     *JUEGO*
                _remainingTime = TIEMPOJUEGO;            // Reinicia el tiempo
                LoadLevel();                             // Instancia el nivel
                _UIManager.SetUpGameHUD(_remainingTime); // Inicializa valores del HUD
                _UIManager.SetMenu(GameStates.GAME);     // Activa HUD
                break;
            case GameStates.GAMEOVER:                    //     *FIN DEL JUEGO*
                UnloadLevel();                           // Descarga el nivel
                _UIManager.SetMenu(GameStates.GAMEOVER); // Activa el texto de GameOver
                break;
        }
        _currentState = newState;                        // Finaliza el cambio
        Debug.Log("CURRENT: " + _currentState);
    }

    private void UpdateState(GameStates state)
    {
        if (state == GameStates.INTRO) // INTRO: únicamente una cuenta atrás
        {
            _introTime -= Time.deltaTime; // Cuenta atrás
            if (_introTime <= 0)
            {
                _nextState = GameStates.GAME; // Pasa a GAME
            }
        }

        if (state == GameStates.GAME) // GAME: cuenta atrás o vidas a 0 + actualizar HUD cada frame
        {
            _remainingTime -= Time.deltaTime; // Cuenta atrás
            if (_remainingTime < 0) // Si se acaba el tiempo o las vidas
            {
                OneDown();
            }

            if (!i_frames)
            {
                tiempo_inmortal = (int)_remainingTime;
            }
            else
            {
                if ((int)_remainingTime == tiempo_inmortal - 3.0)
                {
                    i_frames = false;
                }
            }

            _UIManager.UpdateGameHUD(_remainingTime); // Actualiza la información del HUD cada frame
        }
    }

    public void RequestStateChange(GameStates newState) // Método público para cambiar el valor privado de estado 
    {
        _nextState = newState;
    }

    private void LoadLevel() // Instancia el lvl y ya
    {
        _levelInstance = Instantiate(_level, Vector3.zero, Quaternion.identity);
    }

    private void UnloadLevel() // Lo destruye y ya
    {
        Destroy(_levelInstance);
    }

    #endregion

    private void Awake()
    {
        _instance = this; // Para que este GameManager sea accesible a través de GameManager.Instance en otros scripts y objetos
    }

    void Start()
    {
        _currentState = GameStates.INTRO; // Valor dummy para que se realice el cambio nada más empezar
        _nextState = GameStates.START;    // Estado inicial, es diferente al current para que el EnterState del primer update se realice
        _marioState = MarioStates.PEQUE;  // Inicializandolo 
    }

    void Update()
    {
        if (_nextState != _currentState) // Si se requiere cambiar de estado (si current == next es que seguimos en el mismo)
        {
            EnterState(_nextState);      // Entramos al siguiente estado
        }
        UpdateState(_currentState);      // Update según el estado
    }
}
