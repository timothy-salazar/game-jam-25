using UnityEngine;

namespace GJ2025.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;
        private GameObject currentObj;

        public void StartAction(IAction action, GameObject obj)
        {
            if (currentAction == action && currentObj == obj) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = action;
            currentObj = obj;
        }
    }
}