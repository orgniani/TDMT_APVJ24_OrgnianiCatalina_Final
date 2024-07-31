using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_target;

    [SerializeField] private float m_mouseSensitivity = 2f;
    [SerializeField] private float m_distance = 2f;
    [SerializeField] private float m_height = 1f;
    [SerializeField] private float m_angleOffset = 0f;

    [SerializeField] private float m_minPitch = -89f;
    [SerializeField] private float m_maxPitch = 89f;

    private float m_yaw = 0f;
    private float m_pitch = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (m_target == null)
        {
            Debug.LogError("Target is not assigned.");
            return;
        }

        Vector3 angles = transform.eulerAngles;
        m_yaw = angles.y;
        m_pitch = angles.x;
    }

    private void Update()
    {
        if (m_target == null) return;

        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity;

        m_yaw += mouseX;
        m_pitch -= mouseY;
        m_pitch = Mathf.Clamp(m_pitch, m_minPitch, m_maxPitch); // Clamping pitch
    }

    private void LateUpdate()
    {
        if (m_target == null) return;

        Quaternion rotation = Quaternion.Euler(m_pitch, m_yaw, 0);
        Vector3 position = m_target.position - (rotation * Vector3.forward * m_distance) + (Vector3.up * m_height);

        transform.position = position;
        transform.LookAt(m_target.position + Vector3.up * m_angleOffset);
    }
}