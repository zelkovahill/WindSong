using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private static readonly int MOVE_PARAM = Animator.StringToHash("Move");
    private static readonly int ATTACK_PARAM = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool isMoving)
    {
        _animator.SetBool(MOVE_PARAM, isMoving);
    }

    public void SetAttackAnimation(bool isAttacking)
    {
        _animator.SetBool(ATTACK_PARAM, isAttacking);
    }
}
