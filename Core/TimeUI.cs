using UnityEngine;
using UnityEngine.UI;
using System;

namespace GJ2025.Core
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] Text time;
        [SerializeField] float maxTime = 10f;

        private float currentTime = 0;
        private float currentDay = 1;

        private void Start()
        {

        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            time.text = TimeSpan.FromSeconds(maxTime - currentTime).ToString(@"mm\:ss");

            if (maxTime - currentTime <= 0)
            {
                ResetTime(180f);
            }
        }

        public void ResetTime(float newMaxTime)
        {
            Helpers.OnChangeDay.Invoke();
            maxTime = newMaxTime;
            currentTime = 0;
        }
    }
}