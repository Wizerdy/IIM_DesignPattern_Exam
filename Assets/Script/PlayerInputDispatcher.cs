using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputDispatcher : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;

    [SerializeField] EntityMovement _movement;
    [SerializeField] EntityFire _fire;
    [SerializeField] EntityBlock block;
    [SerializeField] EntityGrab grab;

    [SerializeField] InputActionReference _pointerPosition;
    [SerializeField] InputActionReference _moveJoystick;
    [SerializeField] InputActionReference _fireButton;
    [SerializeField] InputActionReference _blockButton;
    [SerializeField] InputActionReference _grabButton;
    [SerializeField] InputActionReference _dropButton;

    Coroutine MovementTracking { get; set; }

    Vector3 ScreenPositionToWorldPosition(Camera c, Vector2 cursorPosition) => _mainCamera.ScreenToWorldPoint(cursorPosition);

    private void Start()
    {
        // binding
        _fireButton.action.started += FireInput;

        _blockButton.action.started += BlockInput;
        _blockButton.action.canceled += BlockInput;

        _grabButton.action.started += GrabInput;

        _dropButton.action.started += DropInput;

        _moveJoystick.action.started += MoveInput;
        _moveJoystick.action.canceled += MoveInputCancel;
    }

    private void OnDestroy()
    {
        _fireButton.action.started -= FireInput;

        _blockButton.action.started -= BlockInput;
        _blockButton.action.canceled -= BlockInput;

        _grabButton.action.started -= GrabInput;

        _dropButton.action.started -= DropInput;

        _moveJoystick.action.started -= MoveInput;
        _moveJoystick.action.canceled -= MoveInputCancel;
    }

    private void MoveInput(InputAction.CallbackContext obj)
    {
        if (MovementTracking != null) return;

        MovementTracking = StartCoroutine(MovementTrackingRoutine());
        IEnumerator MovementTrackingRoutine()
        {
            while (true)
            {
                _movement.PrepareDirection(obj.ReadValue<Vector2>());
                yield return null;
            }
            yield break;
        }
    }

    private void MoveInputCancel(InputAction.CallbackContext obj)
    {
        if (MovementTracking == null) return;
        _movement.PrepareDirection(Vector2.zero);
        StopCoroutine(MovementTracking);
        MovementTracking = null;
    }

    private void FireInput(InputAction.CallbackContext obj)
    {
        if (block.IsBlocking) { return; }

        float fire = obj.ReadValue<float>();
        if(fire==1)
        {
            _fire.FireBullet(2);
        }
    }

    private void BlockInput(InputAction.CallbackContext obj) {
        float block = obj.ReadValue<float>();
        this.block.Block(block == 1);
    }

    private void GrabInput(InputAction.CallbackContext obj) {
        if (!grab.HasObjectGrabbed) {
            if (grab.GrabNearest()) {
                return;
            }
        }
        grab.Use();
    }

    private void DropInput(InputAction.CallbackContext obj) {
        if (grab.HasObjectGrabbed) {
            grab.Ungrab();
        }
    }
}
