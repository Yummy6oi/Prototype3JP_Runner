using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _dirtParticle;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _crashSound;
    

    [SerializeField] private float _gravityModifier = 1.7f;
    [SerializeField] private float _jumpForce = 9;
    [SerializeField] private int _maxJumpCount = 2;
    private int _currentJumpCount;

    private bool _doubleSpeed;
    public bool DoubleSpeed
    {
        get { return _doubleSpeed; }
        set { _doubleSpeed = value; }
    }
    private bool _gameOver = false;
    public bool GameOver
    {
        get { return _gameOver; }
        set { _gameOver = value; }
    }

    private AudioSource _playerAudio;
    private Rigidbody _playerRigidBody;
    private Animator _playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= _gravityModifier;
        _currentJumpCount = _maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_currentJumpCount > 0) && (!GameOver))
        {
            Jump();    
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _doubleSpeed = true;
            _playerAnim.SetFloat("Speed_Multiplier", 2f);
        }
        else
        {
            _doubleSpeed = false;
            _playerAnim.SetFloat("Speed_Multiplier", 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _currentJumpCount = _maxJumpCount;
            _dirtParticle.Play();
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            ApplyGameOver();   
        }
    }

    public void ApplyGameOver()
    {
        _playerAudio.PlayOneShot(_crashSound, 2f);
        _explosionParticle.Play();
        _dirtParticle.Stop();

        _playerAnim.SetBool("Death_b",true);
        _playerAnim.SetInteger("DeathType_int", 1);

        GameOver = true;
        Debug.Log("GameOver");

    }
    private void Jump()
    {
        _playerAudio.PlayOneShot(_jumpSound, 2f);
        _playerRigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _dirtParticle.Stop();
        _playerAnim.SetTrigger("Jump_trig");

        _currentJumpCount -= 1;       
    }



}
