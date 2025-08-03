using UnityEngine;

namespace GJ2025.Core
{
    public class LookAtCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            float targetXRotation = Camera.main.transform.rotation.eulerAngles.x;
            Vector3 newRotation = new Vector3(targetXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
