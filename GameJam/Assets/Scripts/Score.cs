using UnityEngine;

public class Score : MonoBehaviour
{
    private float _score = 0;
    private float _multiplier = 1;

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

    public float GetScore { get => _score;}

    public void PlayerScore(float _distance, bool weakpoint)
    {
        _score += (10 * _distance) * _multiplier;
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
