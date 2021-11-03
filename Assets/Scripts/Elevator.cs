using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField]
    private Transform _targetA, _targetB, _currentTarget;
    [SerializeField]
    private float _speed = 2f;
    private bool _moveDown, _moveUp;

    private void Start()
    {
        _currentTarget = transform;
    }

    public void CallElevator()
    {
        if (_currentTarget.position == _targetA.position)
        {
            _moveDown = true;
            _moveUp = false;
        }

    }

    private void FixedUpdate()
    {
        if (_moveDown == true)
        {
            _currentTarget = _targetB;
        }
        else if (_moveUp == true)
        {
            _currentTarget = _targetA;
        }

        if(transform.position == _targetB.position)
        {
            StartCoroutine(ElevatorUpRoutine());
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

    IEnumerator ElevatorUpRoutine()
    {
        yield return new WaitForSeconds(1f);

        _moveDown = false;
        _moveUp = true;
    }
}
