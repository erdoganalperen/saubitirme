using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private ScriptableCameraSettings cameraSettings;
    [SerializeField] private Transform positionTarget;
    [SerializeField] private Transform rotationTarget;
    private bool _lockCameraFlow;
    public bool lockFlowManual;
    private float _leftEdge, _rightEdge, _bottomEdge, _upperEdge;
    private InputManager _inputManagerInstance;
    void Start()
    {
        Cursor.lockState = cameraSettings.CursorLockMode;
        _leftEdge = cameraSettings.ScreenEdgePositionOffset;
        _bottomEdge = cameraSettings.ScreenEdgePositionOffset;
        _rightEdge = GameConfig.Instance.screenWidth - cameraSettings.ScreenEdgePositionOffset;
        _upperEdge = GameConfig.Instance.screenHeight - cameraSettings.ScreenEdgePositionOffset;
        _inputManagerInstance = InputManager.Instance;
    }

    void Update()
    {
        if (GameManager.Instance.activeScene != Scenes.GameScene)
        {
            return;
        }
        ProcessMouseButton();
        if (_lockCameraFlow)
        {
            ProcessCameraRotation();
        }
        else if (!_lockCameraFlow)
        {
            if (!_inputManagerInstance.IsMouseOverUI)
            {
                ProcessCameraZoom();
                if (!lockFlowManual && !_inputManagerInstance.lockFlow)
                {
                    ProcessCameraPosition();
                }
            }
        }
    }

    private void ProcessCameraRotation()
    {
        float horizontalInput = _inputManagerInstance.MousePositionDelta.x;
        float verticalInput = _inputManagerInstance.MousePositionDelta.y;

        if (horizontalInput > cameraSettings.RotationOffset || horizontalInput < -cameraSettings.RotationOffset)
        {
            float sign = Mathf.Sign(_inputManagerInstance.MousePositionDelta.x);
            Vector3 eulers = Vector3.up * (cameraSettings.RotationSpeed * sign * Time.deltaTime);
            rotationTarget.Rotate(eulers, Space.World);
        }

        if (verticalInput > cameraSettings.RotationOffset || verticalInput < -cameraSettings.RotationOffset)
        {
            float sign = Mathf.Sign(_inputManagerInstance.MousePositionDelta.y);
            Vector3 eulers = Vector3.left * (cameraSettings.RotationSpeed * sign * Time.deltaTime);
            rotationTarget.Rotate(eulers, Space.Self);
        }
    }
    //
    private void ProcessCameraPosition()
    {
        Vector3 appliedPosition = positionTarget.position;
        float horizontalPositionInput = _inputManagerInstance.MousePosition.x;
        float verticalPositionInput = _inputManagerInstance.MousePosition.y;
        float positionalSpeed = appliedPosition.y / cameraSettings.ZoomOutEdge * cameraSettings.PositionSpeed;
        if (horizontalPositionInput >= _rightEdge || horizontalPositionInput <= _leftEdge)
        {
            float sign = horizontalPositionInput <= _leftEdge ? -1 : 1;
            appliedPosition += positionTarget.TransformDirection(Vector3.right) *
                               (positionalSpeed * sign * Time.deltaTime);
        }
        if (verticalPositionInput >= _upperEdge || verticalPositionInput <= _bottomEdge)
        {
            float sign = verticalPositionInput <= _bottomEdge ? -1 : 1;
            appliedPosition += positionTarget.TransformDirection(Vector3.forward) *
                               (positionalSpeed * sign * Time.deltaTime);
        }
        appliedPosition.y = positionTarget.position.y;
        positionTarget.position = appliedPosition;
    }
    //
    private void ProcessCameraZoom()
    {
        Vector3 appliedZoomPosition = positionTarget.position;
        float zoomInput = _inputManagerInstance.MouseScrollDeltaY;
        if (zoomInput > 0f) //ZoomIn
        {
            appliedZoomPosition += positionTarget.TransformDirection(Vector3.forward) * (Time.deltaTime * cameraSettings.ZoomSpeed);
        }
        else if (zoomInput < 0f) //ZoomOut
        {
            appliedZoomPosition -= positionTarget.TransformDirection(Vector3.forward) * (Time.deltaTime * cameraSettings.ZoomSpeed);
        }
        if (appliedZoomPosition.y < cameraSettings.ZoomInEdge || appliedZoomPosition.y > cameraSettings.ZoomOutEdge)
        {
            appliedZoomPosition = positionTarget.position;
        }

        positionTarget.position = appliedZoomPosition;
    }

    private void ProcessMouseButton()
    {
        if (_inputManagerInstance.RightClick && !_lockCameraFlow)
        {
            _lockCameraFlow = true;
        }
        if (!_inputManagerInstance.RightClick && _lockCameraFlow)
        {
            _lockCameraFlow = false;
        }
    }

    public void SetPosition(Vector2 highest)
    {
        var pos = highest;
        pos *= .5f;
        positionTarget.transform.position = new Vector3(pos.x, 5, pos.y);
    }
}