using UnityEngine;
using UnityEngine.UI;

namespace GJ2025.Core
{
    public class TutorialMenu : MonoBehaviour
    {
        private int page = 0;
        [SerializeField] Transform pages;
        [SerializeField] Button prevButton;
        [SerializeField] Button nextButton;
        [SerializeField] Button closeButton;

        private void OnEnable()
        {
            Time.timeScale = 0f;
            prevButton.onClick.AddListener(PreviousPage);
            nextButton.onClick.AddListener(NextPage);
            closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            prevButton.onClick.RemoveListener(PreviousPage);
            nextButton.onClick.RemoveListener(NextPage);
            closeButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void PreviousPage()
        {
            SetPage(page - 1);
        }

        private void NextPage()
        {
            SetPage(page + 1);
        }

        private void SetPage(int newValue)
        {
            pages.GetChild(page).gameObject.SetActive(false);

            page = Mathf.Max(newValue, 0);
            pages.GetChild(page).gameObject.SetActive(true);

            if (page == 0)
            {
                prevButton.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);
            }
            else if (page == pages.childCount - 1)
            {
                prevButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                prevButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
            }
        }
    }
}
