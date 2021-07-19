using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePrefabs;

    private PlayerController _playerController;
    private Vector3 _spawnPos = new Vector3(25, 0, 0);

    private float _startDelay = 2f;
    private float _repeatDelay = 4f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", _startDelay, _repeatDelay);
        _playerController = FindObjectOfType<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (!_playerController.GameOver)
        {
            int randomObstacle = Random.Range(0, _obstaclePrefabs.Length);
            Instantiate(_obstaclePrefabs[randomObstacle], _spawnPos, _obstaclePrefabs[randomObstacle].transform.rotation);
        }    
    }
}
