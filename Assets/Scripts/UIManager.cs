using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    #region references
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private TMP_Text _coinsTMP;
    [SerializeField] private TMP_Text _remainingTimeTMP;
    [SerializeField] private GameObject _mainMenu;
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

    public void UpdateGameHUD(int score, int coins, float time) 
    {
        _scoreTMP.text = (score + 1).ToString("D6");
        _coinsTMP.text = "x" + (coins + 1).ToString("D2"); // +1 solo como debug para ver si funciona
        _remainingTimeTMP.text = "" + (int)time;
    }

    public void SetUpGameHUD(float time) // Actualiza *sólo cada vez que se carga un nivel* los datos de ese nivel 
    {
        _scoreTMP.text = "000000";
        _coinsTMP.text = "x00";
        _remainingTimeTMP.text = "" + (int)time;
        Debug.Log("SetupHUD***");
    }

    public void SetMenu(GameManager.GameStates newMenu)
    { // desactiva el menú anterior, actualiza el actual y lo activa
        _menus[(int)_activeMenu].SetActive(false);
        _activeMenu = newMenu;
        _menus[(int)_activeMenu].SetActive(true);
    }
    #endregion

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
