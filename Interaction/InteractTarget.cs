using GJ2025.Core;
using UnityEngine;
using UnityEngine.UI;

namespace GJ2025.Interaction
{
    public class InteractTarget : MonoBehaviour
    {
        [SerializeField] Vector3 direction;
        [SerializeField] float maxTime = 3f;
        [SerializeField] Sprite startIcon;
        [SerializeField] Sprite endIcon;

        private Canvas ui;
        private ProgressBarTracker progressBar;

        private void Start()
        {
            ui = GetComponentInChildren<Canvas>();
            progressBar = GetComponentInChildren<ProgressBarTracker>();
            SetupIcons();
        }

        private void Update()
        {
            if (progressBar.IsComplete())
            {
                FinishInteraction();
            }
        }

        public void StartTimer()
        {
            ui.enabled = true;
            progressBar.StartTimer(maxTime);
        }

        public void Cancel()
        {
            progressBar.ResetTimer();
            ui.enabled = false;
        }

        private void SetupIcons()
        {
            Image[] images = GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                switch (image.name)
                {
                    case "Progress Bar":
                        image.sprite = startIcon;
                        break;
                    case "Progress Bar Filling":
                        image.sprite = endIcon;
                        image.type = Image.Type.Filled;
                        image.fillMethod = Image.FillMethod.Radial360;
                        image.fillOrigin = 2;
                        image.fillClockwise = false;
                        progressBar.SetFilling(image);
                        break;
                }
            }
        }

        private void FinishInteraction()
        {
            // Cancel();
        }

        public Vector3 Direction()
        {
            return direction;
        }

        public bool IsComplete()
        {
            return progressBar.IsComplete();
        }
    }

}
