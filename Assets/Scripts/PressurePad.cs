using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{

    [SerializeField]
    private Renderer _padDisplay;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Moving Box"))
        {
            float distance = Vector3.Distance(other.transform.position, transform.position);
            if (distance <= .3f)
            {
                other.attachedRigidbody.isKinematic = true;
                _padDisplay.material.color = Color.blue;
            }
        }
    }
}
