using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score = 0;
    private int _multiplier = 1;

    private static Score instance;

    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Score>();
            }
            return instance;
        }
    }

    public int GetScore { get => _score; }
    public int GetMultiplier { get => _multiplier; }

    public void PlayerScore(float _distance, bool weakpoint)
    {
        _score += (10 * Mathf.RoundToInt(_distance)) * _multiplier;
        if (weakpoint)
            IncrementMultiplier();
    }

    public void ResetMultiplier()
    {
        _multiplier = 1;
    }

    private void IncrementMultiplier()
    {
        if (_multiplier < 5)
        {
            _multiplier ++;
        }
    }
}
