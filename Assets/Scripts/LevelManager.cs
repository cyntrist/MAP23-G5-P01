using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region references
    /// <summary>
    /// Spawn point for player
    /// </summary>
    [SerializeField] private Transform _spawnPoint;
    #endregion

    #region methods
    /// <summary>
    /// Sets player position and rotation. Enables player.
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player)
    {
        // Primero registra este level manager en la instancia del game manager
        GameManager.Instance.RegisterLevelManager(this);

        // Enables the player and sets its transform up
        player.transform.position = _spawnPoint.transform.position;
        player.transform.rotation = _spawnPoint.transform.rotation;
        player.SetActive(true);
    }
    #endregion
}
