using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("Components")]
    public NavMeshAgent agent;
    [Header ("Veriables")]
    private float _maxHealth = 100;
    private float _currentHealth;
    [Header("UI References")]
    [SerializeField] private Image _healthFill;
    [SerializeField] private TMP_Text _healthText;
    private Spawner _spawner;

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthFill.fillAmount = _currentHealth / _maxHealth;
        _spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        //Adds velocity to the player based on input axis from WASD or arrow buttons
        agent.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * agent.speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Get nearest enemy
            GameObject nearestEnemy = _spawner.Enemies.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).FirstOrDefault();
            //If nearest enemy found, destroy it and update enemies in spawner
            if (nearestEnemy != null)
            {
                _spawner.Enemies.Remove(nearestEnemy.gameObject);
                Destroy(nearestEnemy.gameObject);
            }    
        }
    }

    public void Hit()
    {
        _currentHealth--;
        _healthFill.fillAmount = _currentHealth / _maxHealth;
        _healthText.text = $"{_currentHealth}/{_maxHealth}";

        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
