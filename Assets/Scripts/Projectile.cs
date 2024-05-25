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
    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = (FindObjectOfType<Player>().transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timePassed >= _timeOfLife)
        {
            Destroy(gameObject);
        }
        _timePassed += Time.deltaTime;
        transform.position += _moveDirection * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Hit();
        }
        Destroy(gameObject);
    }
}
