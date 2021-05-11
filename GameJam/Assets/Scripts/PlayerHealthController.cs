using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        _healthCurrent -= 1;
        Debug.Log(_healthCurrent);

        if (_healthCurrent == 0)
        {
            SceneManager.LoadScene(0);
        }
        _enemySpawner.enemyList.Remove(other.transform);
        Destroy(other.gameObject);
    }


    #endregion


    #region Private and Protected

    [SerializeField] private int _healthInitial = 3;
    private int _healthCurrent;
    [SerializeField] private EnemySpawner _enemySpawner;

    #endregion
}