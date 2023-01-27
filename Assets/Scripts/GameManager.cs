using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { START, GAME, GAMEOVER };

    #region references
    private UIManager _UIManager;
    [SerializeField] GameObject _player;
    #endregion

    #region properties
    private static GameManager _instance;
    private GameManager.GameStates _currentState;
    private GameManager.GameStates _nextState;
    private float _remainingTime = 60;
    private int _score;
    public static GameManager Instance { get { return _instance; } }
    public GameManager.GameStates CurrentState { get { return _currentState; } }
    public int Score {  get { return _score; } }
    #endregion

    #region methods
    // BLOQUE DE REGISTROS DE REFERENCIAS 
    /// <param name="uiManager">UI manager to register</param>
    public void RegisterUIManager(UIManager uiManager)
    {
        _UIManager = uiManager;
    }

    // BLOQUE DE JUEGO 
    public void AddScore(int points) // Solo add porque creo que nunca resta
    {
        _score += points;
    }

    // BLOQUE DE MÁQUINA DE ESTADOS 
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

    /// <param name="newState">Exited game state</param>
    private void ExitState(GameStates newState)
    {
        if (newState == GameStates.GAME) 
        {
            //
        }
    }

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

    /// <param name="newState">Requested state</param>
    public void RequestStateChange(GameManager.GameStates newState)
    {
        _nextState = newState;  // Método público para cambiar el valor privado de estado 
    }
    #endregion

    private void Awake()
    {
        _instance = this; // Para que éste GameManager sea accesible a través de GameManager.Instance en otros scripts y objetos
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
