using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region references
    /// <summary>
    /// Reference to Main Menu object
    /// </summary>
    [SerializeField] private GameObject _mainMenu;
    /// <summary>
    /// Reference to Gameplay HUD object
    /// </summary>
    [SerializeField] private GameObject _gameplayHUD;
    /// <summary>
    /// Reference to Game Over Menu object
    /// </summary>
    [SerializeField] private GameObject _gameOverMenu;
    #endregion

    #region properties
    /// <summary>
    /// Reference to active menu Game State
    /// </summary>
    private GameManager.GameStates _activeMenu;
    /// <summary>
    /// Menus array
    /// </summary>
    private GameObject[] _menus;
    #endregion

    #region methods
    /// <summary>
    /// Requests new state to GameManager
    /// </summary>
    /// <param name="newState"></param>
    public void RequestStateChange(int newState)
    {
        GameManager.Instance.RequestStateChange((GameManager.GameStates)newState);
    }
    /// <summary>
    /// Update in game HUD
    /// </summary>
    /// <param name="currentApples">Current number of collected apples</param>
    /// <param name="remainingTime">Current remaining time</param>
    public void UpdateGameHUD() // Actualiza en cada frame el valor del tiempo y las manzanas
    {

    }
    /// <summary>
    /// Sets up HUD after Level's load
    /// </summary>
    /// <param name="nRound">Round number</param>
    /// <param name="goal">Level goal</param>
    /// <param name="remainingTime">Remaining time</param>
    public void SetUpGameHUD() // Actualiza *sólo cada vez que se carga un nivel* los datos de ese nivel (Ronda y meta)
    {

    }
    /// <summary>
    /// Sets the required menu according to Game State
    /// </summary>
    /// <param name="newMenu">New menu Game State</param>
    public void SetMenu(GameManager.GameStates newMenu)
    { // desactiva el menú anterior, actualiza el actual y lo activa
        _menus[(int)_activeMenu].SetActive(false);
        _activeMenu = newMenu;
        _menus[(int)_activeMenu].SetActive(true);
    }
    #endregion

    /// <summary>
    /// Menus array initialization and UI Manager registration
    /// </summary>
    void Start()
    {
        _menus = new GameObject[3]; // creación del array de menús y asignación
        _menus[0] = _mainMenu;
        _menus[1] = _gameplayHUD;
        _menus[2] = _gameOverMenu;
        _activeMenu = GameManager.Instance.CurrentState; // asocia el menú actual con el estado actual

        GameManager.Instance.RegisterUIManager(this); // registra este UI manager con la instancia del Game manager
    }
}
