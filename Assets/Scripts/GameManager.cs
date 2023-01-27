using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { START, GAME, GAMEOVER };

    #region references
    /// <summary>
    /// Reference to UI Manager
    /// </summary>
    private UIManager _UIManager;
    /// <summary>
    /// Reference to player
    /// </summary>
    [SerializeField] GameObject _player;
    #endregion

    #region properties
    /// <summary>
    /// Game manager instance
    /// </summary>
    static private GameManager _instance;
    /// <summary>
    /// Public reference to GameManager instance / MÉTODO GETTER para recibir el valor en otros scripts
    /// </summary>
    static public GameManager Instance { get { return _instance; } }
    /// <summary>
    /// Current game state
    /// </summary>
    private GameManager.GameStates _currentState;
    /// <summary>
    /// Next game state
    /// </summary>
    private GameManager.GameStates _nextState;
    /// <summary>
    /// Public access to Current State / MÉTODO GETTER para recibir el valor en otros scripts
    /// </summary>
    public GameManager.GameStates CurrentState { get { return _currentState; } }
    /// <summary>
    /// Level settings: Remaining time
    /// </summary>
    private float _remainingTime = 60;
    #endregion

    #region methods
    /// <summary>
    /// Public method to register UI Manager
    /// </summary>
    /// <param name="uiManager">UI manager to register</param>

    public void RegisterUIManager(UIManager uiManager)
    {
        _UIManager = uiManager;
    }

    /// <summary>
    /// GameManager instance initialization
    /// </summary>
    private void Awake()
    {
        _instance = this; // Para que éste GameManager sea accesible a través de GameManager.Instance en otros scripts y objetos
    }

    /// <summary>
    /// Method to be called when game enters a new state
    /// </summary>
    /// <param name="newState">New state</param>
    public void EnterState(GameStates newState)
    {
        switch (newState) // Diferentes comportamientos según estado al que se entra
        { // En sí, solo cambia el grupo de UI por cada estado y en GAME carga el nivel
            case GameStates.START:
                _UIManager.SetMenu(GameStates.START);
                break;
            case GameStates.GAME:
                _UIManager.SetMenu(GameStates.GAME);
                _UIManager.SetUpGameHUD(); // Inicializa el HUD
                break;
            case GameStates.GAMEOVER:
                _UIManager.SetMenu(GameStates.GAMEOVER);
                break;
        }
        _currentState = newState; // Finaliza el cambio
        Debug.Log("CURRENT: " + _currentState);
    }
    /// <summary>
    /// Methods to be called when a game state is exited
    /// </summary>
    /// <param name="newState">Exited game state</param>
    private void ExitState(GameStates newState)
    {
        if (newState == GameStates.GAME) // simplemente quita el nivel y el jugador en GAME porque en el resto de estados no hace falta nada más?
        {
            
        }
    }
    /// <summary>
    /// Method called to uptate the game manager according to the current state
    /// </summary>
    /// <param name="state">Current game state</param>
    private void UpdateState(GameStates state)
    {
        if (_currentState == GameStates.GAME) // En el resto de estados no hace falta nada de momento
        {
            _remainingTime -= Time.deltaTime; // Cuenta atrás

            if (_remainingTime < 0) // Si se acaba el tiempo, salimos del estado de GAME e intentamos entrar en GAMEOVER
            {
                ExitState(_currentState);
                _nextState = GameStates.GAMEOVER;
            }

            _UIManager.UpdateGameHUD(); // Actualiza la información del HUD cada frame
        }
    }
    /// <summary>
    /// Public method for other scripts to request a game state change
    /// </summary>
    /// <param name="newState">Requested state</param>
    public void RequestStateChange(GameManager.GameStates newState)
    {
        _nextState = newState;  // Método público para cambiar el valor privado de estado / podría llamarse SetNewState tambien?
    }
    #endregion

    /// <summary>
    /// Game state initialization
    /// </summary>
    void Start()
    {
        _currentState = GameStates.GAMEOVER; // Valor dummy
        _nextState = GameStates.START; // Estado inicial, es diferente al current para que el EnterState del primer update se realice
    }

    /// <summary>
    /// Game state transition management.
    /// Current game state update call.
    /// </summary>
    void Update()
    {
        if (_nextState != _currentState) // Si se requiere cambiar de estado ( si current == next es que seguimos en el mismo)
        {
            EnterState(_nextState); // Entramos al siguiente estado
        }
        UpdateState(_currentState); // Update según el estado
    }
}
