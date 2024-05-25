using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownComponent : MonoBehaviour
{
    public event System.Action OnCooldownFinished;
    public float CooldownSeconds { get; set; }

    private float _timeToNextPerform;

    public bool CanPerform => _timeToNextPerform == 0;

    public void ResetCooldown()
    {
        _timeToNextPerform = CooldownSeconds;
    }

    public void HandleCooldown()
    {
        if (_timeToNextPerform == 0)
        {
            return;
        }

        if (_timeToNextPerform < 0)
        {
            _timeToNextPerform = 0;
            OnCooldownFinished?.Invoke();
            return;
        }

        _timeToNextPerform -= Time.deltaTime;
    }
}
