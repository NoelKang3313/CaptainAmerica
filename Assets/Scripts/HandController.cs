using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public InputActionReference GripReference;

    private Animator _animator;
    private float _gripFloat;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        GripHand();
    }

    void GripHand()
    {
        _gripFloat = GripReference.action.ReadValue<float>();
        _animator.SetFloat("Grip", _gripFloat);
    }
}
