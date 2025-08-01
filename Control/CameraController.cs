using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 200f;
    public float zoomSpeed = 20f;
    public float minZoom = 20f;
    public float maxZoom = 60f;

    void Update()
    {
        Pan();
        Zoom();
    }

    private void Pan()
    {
        // WASD Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime / 2;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.fieldOfView -= scrollInput * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
    }
}
