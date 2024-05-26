using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This component is created for calculation of cooldowns
public class CooldownComponent : MonoBehaviour
{
    public event System.Action OnCooldownFinished;
    public float CooldownSeconds { get; set; }

    private float _timeToNextPerform;

    public bool CanPerform => _timeToNextPerform == 0;

    //Sets cooldown
    public void ResetCooldown()
    {
        _timeToNextPerform = CooldownSeconds;
    }

    //Updates the time to next perform
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
