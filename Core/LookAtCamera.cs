using UnityEngine;

namespace GJ2025.Core
{
    public class LookAtCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
