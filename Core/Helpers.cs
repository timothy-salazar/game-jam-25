using UnityEngine;

namespace GJ2025.Core
{
    public static class Helpers
    {
        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
