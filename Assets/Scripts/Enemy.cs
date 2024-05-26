using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;
    public CooldownComponent cooldownComponent;

    [Header("Veriables")]
    private Player _target;
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private GameObject _projectilePrefab;

    void Start()
    {
        //Set reference on player and reset cooldown to shoot
        _target = FindObjectOfType<Player>();
        cooldownComponent.CooldownSeconds = _shootCooldown;
    }

    void Update()
    {
        //Updates destination to move and moves enemy to player
        agent.SetDestination(_target.transform.position);
        RaycastHit hit;
        //Raycast from enemy to player, if enemy sees player he shoot
        if(Physics.Raycast(transform.position, (_target.transform.position - transform.position).normalized, out hit))
        {
            if (hit.collider.tag == "Player" && cooldownComponent.CanPerform)
            {
                Shoot();
                //Reset cooldown
                cooldownComponent.ResetCooldown();
            }
        }
        //Cooldown update
        cooldownComponent.HandleCooldown();
    }

    //Shoot the projectile
    private void Shoot()
    {
        Instantiate(_projectilePrefab, transform.position, transform.rotation);
    }
}
