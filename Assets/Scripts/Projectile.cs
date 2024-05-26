using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _moveDirection;
    private float _timePassed=0;
    private float _timeOfLife = 5;
    [SerializeField] private float _speed = 3f;

    void Start()
    {
        //Set movement direction to projectile
        _moveDirection = (FindObjectOfType<Player>().transform.position - transform.position).normalized;
    }

    void Update()
    {
        //Сheck for the lifetime of the projectile
        if (_timePassed >= _timeOfLife)
        {
            Destroy(gameObject);
        }
        _timePassed += Time.deltaTime;
        //Move projectile in movement direction
        transform.position += _moveDirection * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check for projectile collision with player
        if (other.tag == "Player")
        {
            //Hit the player
            other.GetComponent<Player>().Hit();
        }
        Destroy(gameObject);
    }
}
