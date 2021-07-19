using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 25f;
    private PlayerController _playerController;
    private float leftBound = -15f;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerController.GameOver)
        {
            if (_playerController.DoubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            
        }
        if (transform.position.x <leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
