using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    #region Unity API

    private void Start()
    {
        SetHealth();
    }

    #endregion


    #region Utils

    private void SetHealth()
    {
        _healthCurrent = _healthInitial;
    }

    #endregion


    #region Private and Protected

    [SerializeField] private int _healthInitial = 3;
    private int _healthCurrent;

    #endregion
}
