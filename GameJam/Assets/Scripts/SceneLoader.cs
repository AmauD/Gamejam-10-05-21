using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region unity messages
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    #endregion



    #region public methods

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    #endregion

}
