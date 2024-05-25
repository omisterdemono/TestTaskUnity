using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;
    public CooldownComponent cooldown;

    [Header("Veriables")]
    private Player _target;
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private GameObject _projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<Player>();
        cooldown.CooldownSeconds = _shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(_target.transform.position);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (_target.transform.position - transform.position).normalized, out hit))
        {
            if (hit.collider.tag == "Player" && cooldown.CanPerform)
            {
                Shoot();
                cooldown.ResetCooldown();
            }
        }
        cooldown.HandleCooldown();
    }

    private void Shoot()
    {
        Instantiate(_projectilePrefab, transform.position, transform.rotation);
    }
}
