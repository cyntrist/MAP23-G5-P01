using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { START, GAME, GAMEOVER };
    public enum MarioStates { PEQUE, GRANDE, FUEGO, ESTRELLA} // Esto debería ir en el life component pero lo pongo para aclararme con los metodos 

    #region references
    private UIManager _UIManager;
    private LevelManager _levelManager;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _level;
    #endregion

    #region properties
    private static GameManager _instance;
    private GameManager.GameStates _currentState;
    private GameManager.GameStates _nextState;
    private GameManager.MarioStates _marioState;
    private float _remainingTime = 60;
    private int _lifes = 3;
    private int _score;
    private int _coins;
    public static GameManager Instance { get { return _instance; } }
    public GameStates CurrentState { get { return _currentState; } }
    public MarioStates MarioState { get { return _marioState; } }
    public int Lifes { get { return _lifes; } }
    public int Score { get { return _score; } }
    public int Coins { get { return _coins; } }
    #endregion

    #region methods
    // BLOQUE DE REGISTROS DE REFERENCIAS 
    public void RegisterUIManager(UIManager uiManager)
    {
        _UIManager = uiManager;
    }
    public void RegisterLevelManager(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    // BLOQUE DE JUEGO 
    public void OneUp() // champiñon verde
    {
        _lifes++;
    }

    public void OneDown() // rip 
    {
        _lifes--;
    }
    public void AddScore(int points) // Solo add porque creo que nunca resta
    {
        _score += points;
    }

    public void AddCoins(int coins) // Solo add porque creo que nunca resta
    {
        _coins += coins;
    }
    public void RequestMarioChange(MarioStates state) // Para cambiar directamente a uno
    {
        _marioState = state; // o _marioState += state?
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
            case GameStates.GAME:
                LoadLevel();
                _UIManager.SetMenu(GameStates.GAME);
                _UIManager.SetUpGameHUD(_remainingTime) ; // Inicializa el HUD
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
        if (_currentState == GameStates.GAME) // En el resto de estados no hace falta nada de momento
        {
            _remainingTime -= Time.deltaTime; // Cuenta atrás

            if (_remainingTime < 0 || _lifes <= 0) // Si se acaba el tiempo o las vidas
            {
                //ExitState(_currentState);
                _nextState = GameStates.GAMEOVER;
            }

            _UIManager.UpdateGameHUD(_score, _coins, _remainingTime); // Actualiza la información del HUD cada frame
        }
    }

    public void RequestStateChange(GameManager.GameStates newState)
    {
        _nextState = newState;  // Método público para cambiar el valor privado de estado 
    }

    private void LoadLevel()
    {
        Instantiate(_level, Vector3.zero, Quaternion.identity);

        // Setting the player up
        _levelManager = _level.GetComponent<LevelManager>();
    }

    private void UnloadLevel()
    {
        Destroy(_level);
    }
    #endregion

    private void Awake()
    {
        _instance = this; // Para que éste GameManager sea accesible a través de GameManager.Instance en otros scripts y objetos
        _marioState = MarioStates.PEQUE;
    }

    void Start()
    {
        _currentState = GameStates.GAMEOVER; // Valor dummy para que se realice el cambio nada más empezar
        _nextState = GameStates.START; // Estado inicial, es diferente al current para que el EnterState del primer update se realice
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
