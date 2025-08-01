using System;
using UnityEngine;
using UnityEngine.UI;

namespace GJ2025.Core
{
    public class ProgressBarTracker : MonoBehaviour
    {
        private bool isStarted = false;
        private bool isComplete = false;
        private Image filling;
        private float timeSinceStart = 0;
        private float maxTime;

        private void Update()
        {
            if (!isStarted) return;

            Progress();
        }

        public void StartTimer(float time)
        {
            maxTime = time;
            isStarted = true;
            isComplete = false;
        }

        public void ResetTimer()
        {
            timeSinceStart = 0;
            isStarted = false;
            isComplete = false;
        }

        private void Progress()
        {
            timeSinceStart += Time.deltaTime;
            float progress = Math.Min(timeSinceStart / maxTime, 1);

            if (progress < 1)
            {
                filling.fillAmount = progress;
            }
            else
            {
                isComplete = true;
            }
        }

        public void SetFilling(Image newValue)
        {
            filling = newValue;
        }

        public bool IsComplete()
        {
            return isComplete;
        }
    }
}
