using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerInput _playerInput;
    protected PlayerMovement _playerMovement;
    protected PlayerAnimation _playerAnimation;

    public PlayerState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _playerInput = playerStateMachine.GetComponent<PlayerInput>();
        _playerMovement = playerStateMachine.GetComponent<PlayerMovement>();
        _playerAnimation = playerStateMachine.GetComponent<PlayerAnimation>();
    }

    public abstract void Enter();
    public abstract void FixedUpdate();
    public abstract void Update();
    public abstract void Exit();

    public abstract void Transition();
}

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        Transition();
    }

    public override void Exit()
    {

    }

    public override void Transition()
    {
        if (_playerInput.MoveInput != Vector3.zero)
        {
            _playerStateMachine.TransitionState(_playerStateMachine.PlayerMoveState);
        }

        if (_playerInput.IsAttacking)
        {
            _playerStateMachine.TransitionState(_playerStateMachine.PlayerAttackState);
        }
    }
}

public class PlayerMoveState : PlayerState
{

    public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        _playerAnimation.SetMoveAnimation(true);

    }
    public override void FixedUpdate()
    {

    }
    public override void Update()
    {
        _playerMovement.Move(_playerInput.MoveInput);
        _playerMovement.Rotate(_playerInput.MoveInput);

        Transition();
    }

    public override void Exit()
    {
        _playerAnimation.SetMoveAnimation(false);
    }

    public override void Transition()
    {
        if (_playerInput.MoveInput == Vector3.zero)
        {
            _playerStateMachine.TransitionState(_playerStateMachine.PlayerIdleState);
        }

        if (_playerInput.IsAttacking)
        {
            _playerStateMachine.TransitionState(_playerStateMachine.PlayerAttackState);
        }
    }
}

public class PlayerAttackState : PlayerState
{
    private float _attackDuration = 0.2f;
    private float _attackTimer = 0f;

    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        _attackTimer = 0f;
        _playerAnimation.SetAttackAnimation(true);
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_playerInput.MoveInput != Vector3.zero)
        {
            _playerAnimation.SetMoveAnimation(true);
            _playerMovement.Move(_playerInput.MoveInput);
            _playerMovement.Rotate(_playerInput.MoveInput);
        }
        else
        {
            _playerAnimation.SetMoveAnimation(false);
            _playerMovement.Move(Vector3.zero);
            _playerMovement.Rotate(Vector3.zero);
        }

        Transition();
    }

    public override void Exit()
    {
        _attackTimer = 0f;
        _playerAnimation.SetMoveAnimation(false);
        _playerAnimation.SetAttackAnimation(false);
    }

    public override void Transition()
    {
        if (_attackTimer >= _attackDuration)
        {
            _playerStateMachine.TransitionState(_playerStateMachine.PlayerIdleState);
        }

        if (_playerInput.MoveInput != Vector3.zero)
        {
            _playerStateMachine.TransitionState(_playerStateMachine.PlayerMoveState);
        }
    }
}
