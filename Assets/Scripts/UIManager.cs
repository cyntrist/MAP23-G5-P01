using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region references
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private TMP_Text _coinsTMP;
    [SerializeField] private TMP_Text _remainingTimeTMP;
    [SerializeField] private TMP_Text _scoreMessageTMP; // para enseñar el score de las acciones
    [SerializeField] private TMP_Text _introLivesTMP; // para enseñar las vidas
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _introMessage; // para enseñar las vidas
    [SerializeField] private GameObject _gameplayHUD;
    [SerializeField] private GameObject _gameOverMenu;
    #endregion

    #region properties
    private GameManager.GameStates _activeMenu;
    private GameObject[] _menus;
    #endregion

    #region methods
    public void RequestStateChange(int newState)
    {
        GameManager.Instance.RequestStateChange((GameManager.GameStates)newState);
    }

    public void UpdateGameHUD(float time)
    {
        _scoreTMP.text = (GameManager.Instance.Score).ToString("D6");
        _coinsTMP.text = "x" + (GameManager.Instance.Coins).ToString("D2"); // +1 solo como debug para ver si funciona
        _remainingTimeTMP.text = "" + (int)time;
    }

    public void SetUpGameHUD(float time) // Actualiza *sólo cada vez que se carga un nivel* los datos de ese nivel 
    {
        _scoreTMP.text = "000000";
        _coinsTMP.text = "x00";
        _remainingTimeTMP.text = "" + (int)time;
        GameManager.Instance.AddCoins(1); // para ver si funca
        GameManager.Instance.AddScore(10); // para ver si funca
    }

    public void SetMenu(GameManager.GameStates newMenu)
    { // desactiva el menú anterior, actualiza el actual y lo activa
        _menus[(int)_activeMenu].SetActive(false);
        Debug.Log("Desactivado: " + _activeMenu.ToString());
        _activeMenu = newMenu;
        _menus[(int)_activeMenu].SetActive(true);
        Debug.Log("Activado: " + _activeMenu.ToString());
    }

    public void SetLives(int lives)
    {
        _introLivesTMP.text = "x  " + lives;
    }

    public void ShowScore(int score, bool activate)
    {
        _scoreMessageTMP.text = score.ToString();
        _scoreMessageTMP.enabled = activate;
        //_scoreMessageTMP.transform.position = new(0, 0, 0); 
    }
    #endregion

    void Start()
    {
        _menus = new GameObject[4]; // creación del array de menús y asignación
        _menus[0] = _mainMenu;
        _menus[1] = _introMessage;
        _menus[2] = _gameplayHUD;
        _menus[3] = _gameOverMenu;
        _activeMenu = GameManager.Instance.CurrentState; // asocia el menú actual con el estado actual

        GameManager.Instance.RegisterUIManager(this); // registra este UI manager con la instancia del Game manager
    }
}
