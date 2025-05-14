using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 입력 처리 클래스
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public Vector3 MoveInput { get; private set; }

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private void Update()
    {
        HandleMoveInput();
    }

    private void HandleMoveInput()
    {
        float horizontal = Input.GetAxisRaw(HORIZONTAL);
        float vertical = Input.GetAxisRaw(VERTICAL);
        MoveInput = new Vector3(horizontal, 0, vertical).normalized;
    }
}
