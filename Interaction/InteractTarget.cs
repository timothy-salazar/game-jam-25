using UnityEngine;
using GJ2025.Core;
using UnityEditor;

namespace GJ2025.Interaction
{
    public class InteractTarget : MonoBehaviour
    {
        [SerializeField] Vector3 direction;
        [SerializeField] float maxTime = 3f;
        [SerializeField] bool destroyOnInteract = false;
        [SerializeField] bool isBottomless = false;
        [SerializeField] Ingredient resultIngredient;
        [SerializeField] Ingredient[] possibleIngredients;
        [SerializeField] IngredientStorage[] neededIngredients;

        private bool isInteracting = false;
        private Interacter interacter;
        private Canvas ui;
        private ProgressBarTracker progressBar;
        private InteractFloor floor;
        private ProgressBarTracker[] neededIngredientsUI;

        private void Start()
        {
            progressBar = GetComponentInChildren<ProgressBarTracker>();
            ui = progressBar.GetComponent<Canvas>();
            floor = GetComponentInChildren<InteractFloor>();

            if (resultIngredient == null) SetResultingIngredient();
            if (floor != null) floor.SetupIcon(resultIngredient.Icon(), resultIngredient.YOffset());
            if (progressBar != null) progressBar.SetupIcon(resultIngredient.Icon(), resultIngredient.YOffset());
            if (neededIngredients.Length > 0) SetupDropOffIcons();
        }

        private void Update()
        {
            if (neededIngredients.Length > 0) FillNeededIngredients(false);
            if (!isInteracting) return;
            if (progressBar.IsComplete())
            {
                FinishInteraction(true);
            }
        }

        public void StartInteraction(Interacter unit)
        {
            interacter = unit;
            if (maxTime == 0 && !IsBottomless())
            {
                FinishInteraction(true);
                return;
            }

            Ingredient ingredient = interacter.Ingredient();
            if (ingredient)
            {
                DropOff();
                if (!HaveAllIngredients())
                {
                    FinishInteraction(false);
                    return;
                }
            }

            isInteracting = true;
            ui.enabled = true;
            progressBar.StartTimer(maxTime);
        }

        public void Cancel()
        {
            interacter = null;
            isInteracting = false;
            progressBar.ResetTimer();
            ui.enabled = false;
        }

        public void SetResultingIngredient(Ingredient newValue = null)
        {
            if (newValue == null)
            {
                resultIngredient = possibleIngredients[Random.Range(0, possibleIngredients.Length - 1)];
            }
            else
            {
                resultIngredient = newValue;
            }
        }

        private void SetupDropOffIcons()
        {
            GameObject needIngredientsParent = transform.Find("Interact Ingredients").GetChild(neededIngredients.Length - 1).gameObject;

            if (needIngredientsParent != null)
            {
                needIngredientsParent.SetActive(true);
                neededIngredientsUI = needIngredientsParent.GetComponentsInChildren<ProgressBarTracker>();
                FillNeededIngredients(true);
            }
        }

        private void FillNeededIngredients(bool isSetup)
        {
            for (int i = 0; i < neededIngredientsUI.Length; i++)
            {
                if (isSetup) neededIngredientsUI[i].SetupIcon(neededIngredients[i].ingredient.Icon(), neededIngredients[i].ingredient.YOffset());

                neededIngredientsUI[i].SetFill(neededIngredients[i].haveIngredient ? 1 : 0);
            }
        }

        private void FinishInteraction(bool giveResultIngredient)
        {
            if (giveResultIngredient)
            {
                if (resultIngredient.UnitTag() == "")
                {
                    interacter.SetIngredient(resultIngredient);
                    ResetNeededIngredients();
                }
                else
                {
                    Helpers.OnUnitUnlock.Invoke(resultIngredient.UnitTag());
                }
            }
            interacter.GetComponent<ActionScheduler>().StartAction(null, null);
            if (destroyOnInteract) Destroy(transform.root.gameObject);
        }

        private void ResetNeededIngredients()
        {
            for (int i = 0; i < neededIngredients.Length; i++)
            {
                IngredientStorage neededIngredient = neededIngredients[i];
                neededIngredient.haveIngredient = false;
                neededIngredients[i] = neededIngredient;
            }
        }

        private void DropOff()
        {
            if (isBottomless)
            {
                Helpers.OnChangeScore.Invoke(interacter.Ingredient().Score());
                interacter.SetIngredient(null);
            }
            else
            {
                int ingredientStorageIndex = System.Array.FindIndex(neededIngredients, neededIngredient => neededIngredient.ingredient == interacter.Ingredient());
                if (ingredientStorageIndex != -1)
                {
                    neededIngredients[ingredientStorageIndex].haveIngredient = true;
                    interacter.SetIngredient(null);
                }
            }
        }

        public bool HaveAllIngredients()
        {
            return neededIngredients.Length > 0 && System.Array.FindAll(neededIngredients, neededIngredient => !neededIngredient.haveIngredient).Length == 0;
        }

        public Vector3 Direction()
        {
            return direction;
        }

        public bool IsComplete()
        {
            return progressBar.IsComplete();
        }

        public bool IsBottomless()
        {
            return isBottomless;
        }

        public IngredientStorage[] NeededIngredients()
        {
            return neededIngredients;
        }
    }

}
