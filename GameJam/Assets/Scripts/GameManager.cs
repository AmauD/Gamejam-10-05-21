using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields
    [SerializeField] private SceneLoader _sceneLoader;

    #endregion


    #region unity messages

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion



    #region public Methods

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
