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

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthFill.fillAmount = _currentHealth / _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        agent.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * agent.speed;
        if (_currentHealth <= 0) 
        {
            SceneManager.LoadScene("MainScene");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Enemy nearestEnemy = FindObjectsOfType<Enemy>().ToList()
                .OrderBy(e => Vector3.Distance(e.transform.position, transform.position))
                .FirstOrDefault();
            if (nearestEnemy != null)
            {
                FindObjectOfType<Spawner>().Enemies.Remove(nearestEnemy.gameObject);
                Destroy(nearestEnemy.gameObject);
            }    
        }
    }

    public void Hit()
    {
        _currentHealth--;
        _healthFill.fillAmount = _currentHealth / _maxHealth;
        _healthText.text = $"{_currentHealth}/{_maxHealth}";
    }
}
