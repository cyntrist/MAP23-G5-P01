using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { START, GAME, GAMEOVER };

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
    private float _remainingTime = 60;
    private int _score;
    private int _coins;
    public static GameManager Instance { get { return _instance; } }
    public GameManager.GameStates CurrentState { get { return _currentState; } }
    public int Score {  get { return _score; } }
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
    public void AddScore(int points) // Solo add porque creo que nunca resta
    {
        _score += points;
    }

    public void AddCoins(int coins) // Solo add porque creo que nunca resta
    {
        _coins += coins;
    }

    // BLOQUE DE M�QUINA DE ESTADOS 
    public void EnterState(GameStates newState)
    {
        switch (newState) // Diferentes comportamientos seg�n estado al que se entra
        { // En s�, solo cambia el grupo de UI por cada estado y en GAME carga el nivel
            case GameStates.START:
                _UIManager.SetMenu(GameStates.START);
                break;
            case GameStates.GAME:
                LoadLevel();
                _UIManager.SetMenu(GameStates.GAME);
                _UIManager.SetUpGameHUD(_remainingTime) ; // Inicializa el HUD
                break;
            case GameStates.GAMEOVER:
                _UIManager.SetMenu(GameStates.GAMEOVER);
                break;
        }
        _currentState = newState; // Finaliza el cambio
        Debug.Log("CURRENT: " + _currentState);
    }

    private void ExitState(GameStates newState)
    {
        if (newState == GameStates.GAME) 
        {
            //�
        }
    }

    private void UpdateState(GameStates state)
    {
        if (_currentState == GameStates.GAME) // En el resto de estados no hace falta nada de momento
        {
            _remainingTime -= Time.deltaTime; // Cuenta atr�s

            if (_remainingTime < 0) // Si se acaba el tiempo, salimos del estado de GAME e intentamos entrar en GAMEOVER
            {
                ExitState(_currentState);
                _nextState = GameStates.GAMEOVER;
            }

            _UIManager.UpdateGameHUD(_score, _coins, _remainingTime); // Actualiza la informaci�n del HUD cada frame
        }
    }

    public void RequestStateChange(GameManager.GameStates newState)
    {
        _nextState = newState;  // M�todo p�blico para cambiar el valor privado de estado 
    }

    private void LoadLevel()
    {
        Instantiate(_level, Vector3.zero, Quaternion.identity);

        // Setting the player up
        _levelManager = _level.GetComponent<LevelManager>();
        _levelManager.SetPlayer(_player);
        _player.SetActive(true);
    }
    #endregion

    private void Awake()
    {
        _instance = this; // Para que �ste GameManager sea accesible a trav�s de GameManager.Instance en otros scripts y objetos
    }

    void Start()
    {
        _currentState = GameStates.GAMEOVER; // Valor dummy para que se realice el cambio nada m�s empezar
        _nextState = GameStates.START; // Estado inicial, es diferente al current para que el EnterState del primer update se realice
    }

    void Update()
    {
        if (_nextState != _currentState) // Si se requiere cambiar de estado (si current == next es que seguimos en el mismo)
        {
            EnterState(_nextState); // Entramos al siguiente estado
        }
        UpdateState(_currentState); // Update seg�n el estado
    }
}
