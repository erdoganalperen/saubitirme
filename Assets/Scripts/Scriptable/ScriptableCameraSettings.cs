using UnityEngine;
[CreateAssetMenu(menuName = "Settings/Camera Settings")]
public class ScriptableCameraSettings : ScriptableObject
{
    [SerializeField] private CursorLockMode cursorLockMode;
    public CursorLockMode CursorLockMode { get { return cursorLockMode; } }
    
    [Header("Camera Position Settings")]
    [SerializeField] private float screenEdgePositionOffset;
    public float ScreenEdgePositionOffset { get { return screenEdgePositionOffset; } }
    [SerializeField] private float positionSpeed;
    public float PositionSpeed { get { return positionSpeed; } }
    
    [Header("Camera Rotation Settings")]
    [SerializeField] private float rotationSpeed;
    public float RotationSpeed { get { return rotationSpeed; } }
    [SerializeField] private float rotationOffset;
    public float RotationOffset { get { return rotationOffset; } }
    
    [Header("Camera Zoom Settings")]
    [SerializeField] private float zoomSpeed;
    public float ZoomSpeed { get { return zoomSpeed; } }
    [SerializeField] private float zoomOutEdge;
    public float ZoomOutEdge { get { return zoomOutEdge; } }
    [SerializeField] private float zoomInEdge;
    public float ZoomInEdge { get { return zoomInEdge; } }
}