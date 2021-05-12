using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields
    [SerializeField] private SceneLoader _sceneLoader;
    public int _lastScore = 0;

    #endregion


    #region unity messages

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion



    #region public Methods

    public void GameOver()
    {
        // _lastScore = Score.Instance.GetScore();
    }

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void RestartGame()
    {
        _sceneLoader.LoadLevel(1);
    }

    #endregion
}
