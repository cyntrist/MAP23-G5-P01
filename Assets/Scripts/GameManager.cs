using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { START, INTRO, GAME, GAMEOVER };
    public enum MarioStates { PEQUE, GRANDE, FUEGO, ESTRELLA };

    #region references
    private UIManager _UIManager;
    [SerializeField] GameObject _level;
    #endregion

    #region properties
    private static GameManager _instance;
    private GameStates _currentState;
    private GameStates _nextState;
    private static MarioStates _marioState;
    private float _remainingTime = 60;
    private float _introTime = 3; // Segundos que dura la pantalla en negro al cargar el nivel
    private int _lives = 3;
    private int _score;
    private int _coins;
    public static GameManager Instance { get { return _instance; } }
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

    // BLOQUE DE JUEGO 
    public void OneUp() // champiñon verde / monedas
    {
        _lives++;
    }
    public void OneDown() // rip 
    {
        _lives--;
        _nextState = GameStates.INTRO; // Restart
    }
    public void AddScore(int points) // Solo add porque creo que nunca resta?
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

    // BLOQUE DE MÁQUINA DE ESTADOS 
    public void EnterState(GameStates newState)
    {
        switch (newState) // Diferentes comportamientos según estado al que se entra
        { // En sí, solo cambia el grupo de UI por cada estado y en GAME carga el nivel
            case GameStates.START:
                _UIManager.SetMenu(GameStates.START);
                break;
            case GameStates.INTRO:
                _UIManager.SetLives(_lives);
                _UIManager.SetMenu(GameStates.INTRO);
                break;
            case GameStates.GAME:
                LoadLevel();
                _UIManager.SetMenu(GameStates.GAME);
                _UIManager.SetUpGameHUD(_remainingTime); // Inicializa el HUD
                break;
            case GameStates.GAMEOVER:
                UnloadLevel();
                _UIManager.SetMenu(GameStates.GAMEOVER);
                break;
        }
        _currentState = newState; // Finaliza el cambio
        Debug.Log("CURRENT: " + _currentState);
    }

    /*
    private void ExitState(GameStates newState)
    {
        if (newState == GameStates.GAME) 
        {
            //ª
        }
    }
    */

    private void UpdateState(GameStates state)
    {
        if (_currentState == GameStates.INTRO)
        {
            _introTime -= Time.deltaTime;
            if (_introTime <= 0)
            {
                _nextState = GameStates.GAME;
            }
        }

        if (_currentState == GameStates.GAME) // En el resto de estados no hace falta nada de momento
        {
            _remainingTime -= Time.deltaTime; // Cuenta atrás

            if (_remainingTime < 0 || _lives <= 0) // Si se acaba el tiempo o las vidas
            {
                _nextState = GameStates.GAMEOVER;
            }

            _UIManager.UpdateGameHUD(_remainingTime); // Actualiza la información del HUD cada frame
        }
    }

    public void RequestStateChange(GameStates newState)
    {
        _nextState = newState;  // Método público para cambiar el valor privado de estado 
    }

    private void LoadLevel()
    {
        Instantiate(_level, Vector3.zero, Quaternion.identity);
    }

    private void UnloadLevel()
    {
        Destroy(_level);
    }
    #endregion

    private void Awake()
    {
        _instance = this; // Para que éste GameManager sea accesible a través de GameManager.Instance en otros scripts y objetos
    }

    void Start()
    {
        _currentState = GameStates.INTRO; // Valor dummy para que se realice el cambio nada más empezar
        _nextState = GameStates.START; // Estado inicial, es diferente al current para que el EnterState del primer update se realice
        _marioState = MarioStates.PEQUE; // Inicializandolo para el LifeComp
    }

    void Update()
    {
        if (_nextState != _currentState) // Si se requiere cambiar de estado (si current == next es que seguimos en el mismo)
        {
            EnterState(_nextState); // Entramos al siguiente estado
        }
        UpdateState(_currentState); // Update según el estado
    }
}
