using UnityEngine;
using UnityEngine.UI;
using GJ2025.Core;

namespace GJ2025.Interaction
{
    public class ProgressBarTracker : MonoBehaviour, IInteract
    {
        [SerializeField] Image baseImage;
        [SerializeField] Image fillImage;
        [SerializeField] Image fillContainer;

        private bool isStarted = false;
        private bool isComplete = false;
        private float timeSinceStart = 0;
        private float maxTime;

        private void Update()
        {
            if (!isStarted) return;

            Progress();
        }

        public void SetupIcon(Sprite icon, float yOffset)
        {
            baseImage.sprite = icon;
            baseImage.rectTransform.localPosition = new Vector3(0, yOffset, 0);
            fillImage.sprite = icon;
            fillImage.rectTransform.localPosition = new Vector3(0, yOffset, 0);

            fillContainer.type = Image.Type.Filled;
            fillContainer.fillMethod = Image.FillMethod.Radial360;
            fillContainer.fillOrigin = 2;
            fillContainer.fillClockwise = true;
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
            float progress = Mathf.Min(timeSinceStart / maxTime, 1);

            if (progress < 1)
            {
                fillContainer.fillAmount = progress;
            }
            else
            {
                isComplete = true;
            }
        }

        public bool IsComplete()
        {
            return isComplete;
        }

        public void SetFill(float newValue)
        {
            fillContainer.fillAmount = Mathf.Clamp(newValue, 0, 1f);
        }
    }
}
