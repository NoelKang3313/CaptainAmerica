using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainController : MonoBehaviour
{
    private Animator _animator;

    private string currentAnimation;

    const string VILLAIN_IDLE = "Idle_Shoot_Ar";
    const string VILLAIN_ATTACK = "Shoot_SingleShot_AR";
    const string VILLAIN_DEAD = "Die";

    public Transform PlayerPos;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        VillainDeath();

        transform.LookAt(PlayerPos.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            ChangeAnimationState(VILLAIN_DEAD);
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation)
            return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    void VillainDeath()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.5f)
        {
            Destroy(gameObject);
        }
    }
}