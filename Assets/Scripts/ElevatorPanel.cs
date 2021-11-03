using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{

    [SerializeField]
    private Renderer _callButtonRenderer;
    [SerializeField]
    private int _coinsNeeded = 8;
    [SerializeField]
    private Elevator _elevator;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.R) && other.GetComponent<Player>().CoinsAmount() >= _coinsNeeded)
            {
                _callButtonRenderer.material.color = Color.blue;
                _elevator.CallElevator();
            }
        }
    }
}
