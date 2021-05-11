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
            if(SceneManager.sceneCount > 0)
            { 
                SceneManager.LoadScene(0);
            }
        }
        if(_enemySpawner)
        {
            _enemySpawner.enemyList.Remove(other.transform);
        }
        else
        {
            Debug.Log("enemySpawner non assignée sur le PlayerHealthController");
        }
        Destroy(other.gameObject);
    }


    #endregion


    #region Private and Protected

    [SerializeField] private int _healthInitial = 3;
    private int _healthCurrent;
    [SerializeField] private EnemySpawner _enemySpawner;

    #endregion
}