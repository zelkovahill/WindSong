using UnityEngine;

/// <summary>
/// 플레이어 상태 머신 클래스
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }

    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }

    private void Awake()
    {
        PlayerIdleState = new PlayerIdleState(this);
        PlayerMoveState = new PlayerMoveState(this);
    }

    public void Start()
    {
        TransitionState(PlayerIdleState);
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdate();
        }
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public void TransitionState(PlayerState newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.Enter();
        }
    }
}
