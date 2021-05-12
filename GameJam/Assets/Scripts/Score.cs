using UnityEngine;

public class Score : MonoBehaviour
{
    private float _score = 0;
    private float _multiplier = 1;


    public void PlayerScore(float _distance)
    {
        _score += (10 * _distance) * _multiplier;
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
