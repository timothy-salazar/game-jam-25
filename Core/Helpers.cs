using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace GJ2025.Core
{
    public static class Helpers
    {
        public static UnityEvent<string> OnUnitUnlock = new();
        public static UnityEvent<int> OnChangeScore = new();
        public static UnityEvent OnChangeDay = new();

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
