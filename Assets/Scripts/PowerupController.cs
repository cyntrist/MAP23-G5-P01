using UnityEngine;

public class PowerupController : MonoBehaviour
{

    #region parameters
    [SerializeField]
    private float _escalaPowerup = 1.5f;

    #endregion

    #region methods
    public void powerUpGrande()
    {
        Vector3 theScale = transform.localScale;
        theScale.y *= _escalaPowerup;
        transform.localScale = theScale;
        GameManager._marioState = GameManager.MarioStates.GRANDE;
    }

    public void powerDownGrande()
    {
        Vector3 theScale = transform.localScale;
        theScale.y /= _escalaPowerup;
        transform.localScale = theScale;
        GameManager._marioState = GameManager.MarioStates.PEQUE;
    }

    public void powerUpFuego()
    {

    }

    private void powerDownFire()
    {
        //_myDisparaFuego.SetActive(false);
    }

    #endregion

    void Start()
    {
        //_myDisparaFuego= _player.GetComponent<DisparaFuego>;
    }
}
