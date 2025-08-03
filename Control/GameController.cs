using UnityEngine;
using UnityEngine.UI;
using GJ2025.Core;
using GJ2025.Interaction;

namespace GJ2025.Control
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] UnitController selectedUnit;
        [SerializeField] GameObject droppedIngredient;
        [SerializeField] float dropDistance = 5f;
        [SerializeField] int score = 0;
        [SerializeField] int maxScore = 100;
        [SerializeField] GameObject menu;
        [SerializeField] GameObject timeUI;
        [SerializeField] Text dayUI;

        private int currentDay = 1;
        private bool isNight = false;

        private void Start()
        {
            Helpers.OnChangeScore.AddListener(UpdateScore);
            Helpers.OnChangeDay.AddListener(UpdateDay);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu.SetActive(true);
            }

            if (Input.GetMouseButtonDown(1))
            {
                DropIngredient();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Select()) return;
                if (selectedUnit == null) return;
                if (selectedUnit.Interact()) return;
                if (selectedUnit.Move()) return;
            }
        }

        public void CloseMenu(GameObject menu)
        {
            menu.SetActive(false);
        }

        private bool Select()
        {
            RaycastHit[] hits = Physics.RaycastAll(Helpers.GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                UnitController unit = hit.transform.GetComponent<UnitController>();
                if (unit != null && unit.IsSelectable())
                {
                    selectedUnit = unit;
                    unit.SetIsSelected(true);

                    return true;
                }
            }

            return false;
        }

        private void DropIngredient()
        {
            if (selectedUnit == null) return;

            Interacter interacter = selectedUnit.GetComponent<Interacter>();
            Ingredient ingredient = interacter.Ingredient();
            if (ingredient == null) return;

            GameObject recentDrop = ingredient.Drop(droppedIngredient, selectedUnit.transform.position + selectedUnit.transform.forward * dropDistance + Vector3.up * 2.5f);
            recentDrop.GetComponentInChildren<InteractTarget>().SetResultingIngredient(ingredient);
            interacter.SetIngredient(null);
        }

        private void UpdateScore(int increment)
        {
            score += increment;
            score = Mathf.Clamp(score, 0, maxScore);
        }

        private void UpdateDay()
        {
            isNight = !isNight;
            if (!isNight)
            {
                currentDay++;
                dayUI.text = $"{(isNight ? "Night" : "Day")} {currentDay}";
                dayUI.transform.root.gameObject.SetActive(true);
            }
        }
    }
}
