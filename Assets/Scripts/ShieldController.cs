using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldController : MonoBehaviour
{
    public InputActionReference DetachShieldReference;
    public InputActionReference ReturnShieldInputReference;
    public Transform LeftHand;
    public Transform AttachPosition;

    private bool isShieldAttached;
    private Rigidbody _rigidbody;

    public Transform PlayerPos;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        ReturnShieldInputReference.action.started += context => ReturnShield();

        DetachShieldReference.action.started += context => DetachShieldFromHand();
        DetachShieldReference.action.canceled += context => AttachShieldFromHand();
    }

    void ReturnShield()
    {
        float _shieldSpeed = _rigidbody.velocity.magnitude;

        _rigidbody.velocity = Vector3.zero;

        Vector3 _direction = (PlayerPos.position - transform.position).normalized;
        _rigidbody.velocity = _direction * _shieldSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Villain"))
        {
            float _shieldSpeed = _rigidbody.velocity.magnitude;

            _rigidbody.velocity = Vector3.zero;

            Vector3 _direction = (PlayerPos.position - transform.position).normalized;
            _rigidbody.velocity = _direction * _shieldSpeed;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AttachShield"))
        {
            isShieldAttached = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AttachShield"))
        {
            isShieldAttached = false;
        }
    }

    void DetachShieldFromHand()
    {
        gameObject.transform.SetParent(null);
        _rigidbody.isKinematic = false;
    }

    void AttachShieldFromHand()
    {
        if(isShieldAttached)
        {
            gameObject.transform.SetParent(LeftHand);
            _rigidbody.isKinematic = true;
            transform.position = AttachPosition.position;
        }
    }
}
