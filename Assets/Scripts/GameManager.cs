using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _score;
    private PlayerController _playerController;

    [SerializeField] private Transform _startingPoint;
    [SerializeField] private float _lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _score = 0;

        _playerController.GameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if ((_playerController.DoubleSpeed) && (!_playerController.GameOver))
        {
            _score += 2;
        }
        else
        {
            _score++;
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = _playerController.transform.position;
        Vector3 endPos = _startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * _lerpSpeed;
        float fractionJourney = distanceCovered / journeyLength;

        _playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionJourney < 1)
        {
             distanceCovered = (Time.time - startTime) * _lerpSpeed;
             fractionJourney = distanceCovered / journeyLength;

            _playerController.transform.position = Vector3.Lerp(startPos, endPos, fractionJourney);
            yield return null;

        }

        _playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier",1.0f);
        _playerController.GameOver = false;
        StopCoroutine(PlayIntro());

    }
}
