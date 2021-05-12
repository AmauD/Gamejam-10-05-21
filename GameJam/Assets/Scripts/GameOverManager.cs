using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    #region fields
    private GameManager _gameManager;

    #endregion


    #region Unity messages

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    #endregion


    #region publics methods

    public void Quit()
    {
        _gameManager.GameQuit();
    }

    public void Restart()
    {
        _gameManager.RestartGame();
    }

    #endregion


    #region Private and Protected

    #endregion
}
