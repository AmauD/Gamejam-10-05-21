using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    #region fields
    private GameManager _gameManager;
    public TextMeshPro scoreText;

    #endregion


    #region Unity messages

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (scoreText)
            scoreText.text = GameManager.Instance._lastScore.ToString();
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
