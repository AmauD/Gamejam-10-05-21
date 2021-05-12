using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields
    [SerializeField] private SceneLoader _sceneLoader;
    private static GameManager instance;
    public int _lastScore = 0;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

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
        _lastScore = Score.Instance.GetScore;
        _sceneLoader.LoadLevel(2);
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
