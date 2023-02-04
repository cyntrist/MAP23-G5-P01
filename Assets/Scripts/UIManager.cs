using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region references
    [SerializeField] private TMP_Text _scoreTMP;         // GAMEHUD: textbox para ense�ar la puntuaci�n total
    [SerializeField] private TMP_Text _coinsTMP;         // GAMEHUD: textbox para ense�ar las monedas
    [SerializeField] private TMP_Text _remainingTimeTMP; // GAMEHUD: textbox para ense�ar el contrareloj
    [SerializeField] private TMP_Text _scoreMessageTMP;  // GAMEHUD: textbox para ense�ar el score de las acciones
    [SerializeField] private TMP_Text _introLivesTMP;    // INTRO: textbox para ense�ar las vidas
    [SerializeField] private GameObject _mainMenu;       // START UI
    [SerializeField] private GameObject _introMessage;   // INTRO UI
    [SerializeField] private GameObject _gameplayHUD;    // GAME UI
    [SerializeField] private GameObject _gameOverMenu;   // GAME OVER UI
    #endregion

    #region properties
    private GameManager.GameStates _activeMenu;          // Men� actual
    private GameObject[] _menus;                         // Array de men�s totales
    #endregion

    #region methods
    public void RequestStateChange(int newState) // OnClick() del ButtonPrueba aunque ponga que tiene 0 referencias
    {
        GameManager.Instance.RequestStateChange((GameManager.GameStates)newState);
    }

    public void UpdateGameHUD(float time) // Actualiza en cada frame los datos del HUD
    {
        _scoreTMP.text = (GameManager.Instance.Score).ToString("D6");
        _coinsTMP.text = "x" + (GameManager.Instance.Coins).ToString("D2");
        _remainingTimeTMP.text = "" + (int)time;
    }

    public void SetUpGameHUD(float time) // Inicializa el HUD a 0 + tiempo total
    {
        _scoreTMP.text = "000000";
        _coinsTMP.text = "x00";
        _remainingTimeTMP.text = "" + (int)time;
        GameManager.Instance.AddCoins(1); // para ver si funca
        GameManager.Instance.AddScore(10); // para ver si funca
    }

    public void SetMenu(GameManager.GameStates newMenu)  // Desactiva el men� anterior, actualiza el actual y lo activa
    {
        _menus[(int)_activeMenu].SetActive(false);
        Debug.Log("Desactivado: " + _activeMenu.ToString());
        _activeMenu = newMenu;
        _menus[(int)_activeMenu].SetActive(true);
        Debug.Log("Activado: " + _activeMenu.ToString());
    }

    public void SetLives(int lives) // Actualiza la cantidad de vidas en el estado INTRO
    {
        _introLivesTMP.text = "x  " + lives;
    }

    public void ShowScore(int score, bool activate) // TO-DO: que se active, salga encima de mario/en medio de la pantalla?
                                                    // y suba hasta desactivarse pasado x tiempo
    {
        _scoreMessageTMP.text = score.ToString();
        _scoreMessageTMP.enabled = activate;
        //_scoreMessageTMP.transform.position = new(0, 0, 0); 
    }
    #endregion

    void Start()
    {
        _menus = new GameObject[4]; // creaci�n del array de men�s y asignaci�n
        _menus[0] = _mainMenu;
        _menus[1] = _introMessage;
        _menus[2] = _gameplayHUD;
        _menus[3] = _gameOverMenu;
        _activeMenu = GameManager.Instance.CurrentState; // asocia el men� actual con el estado actual

        GameManager.Instance.RegisterUIManager(this); // registra este UI manager con la instancia del Game manager
    }
}
