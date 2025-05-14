using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
    }

    public void Move(Vector3 moveInput)
    {
        Vector3 moveDirection = moveInput;

        transform.position += moveDirection * _playerData.MoveSpeed * Time.deltaTime;
    }

    public void Rotate(Vector3 MoveInput)
    {
        if (MoveInput == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(MoveInput);
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _playerData.RotationSpeed * Time.deltaTime);
        transform.rotation = newRotation;
    }
}
