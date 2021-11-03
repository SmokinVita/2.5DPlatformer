using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _respawnPoint;

    private CharacterController _charController;

    [SerializeField]
    private float _playerSpeed = 5f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15f;

    [SerializeField]
    private int _coins = 0;

    [SerializeField]
    private float _pushPower = 2f;

    private Vector3 _direction, _velocity, _wallJumpDirection;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    private bool _canWallJump = false;

    void Start()
    {
        _charController = GetComponent<CharacterController>();

        UIManager.Instance.UpdateLivesCount(_lives);
    }


    void Update()
    {
        Movement();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //check for moving box
        //confirm it has a rigidbody
        if (hit.collider.CompareTag("Moving Box"))
        {
            if(hit.collider.attachedRigidbody == null)
            {
                Debug.Log($"{hit.collider.name} Does not have a rigidbody.");
                return;
            }

            //push power -- declare variable on top

            //calculate move direction
            if(hit.moveDirection.y <= -3.0f)
            {
                return;
            }
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0);
            //push moving box velocity
            hit.collider.attachedRigidbody.velocity = pushDir * _pushPower;

        }

        if (!_charController.isGrounded && hit.collider.CompareTag("Wall"))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallJumpDirection = hit.normal;
            _canWallJump = true;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");


        if (_charController.isGrounded == true)
        {
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _playerSpeed;
            _canWallJump = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
            {
                _yVelocity = _jumpHeight;
                _velocity = _wallJumpDirection * _playerSpeed;
            }
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _charController.Move(_velocity * Time.deltaTime);
    }

    public void Damage()
    {
        _lives--;

        UIManager.Instance.UpdateLivesCount(_lives);

        if (_lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            _charController.enabled = false;
            transform.position = _respawnPoint.transform.position;
        }

        _charController.enabled = true;
    }

    public void UpdateCoins()
    {
        _coins++;
        UIManager.Instance.UpdateCoinAmount(_coins);
    }

    public int CoinsAmount()
    {
        return _coins;
    }
}
