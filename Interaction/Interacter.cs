using UnityEngine;
using GJ2025.Core;
using GJ2025.Movement;

namespace GJ2025.Interaction
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(ActionScheduler))]

    public class Interacter : MonoBehaviour, IAction
    {
        [SerializeField] float range = 3f;
        [SerializeField] Ingredient ingredient;

        private Mover mover;
        private Animator animator;
        private ActionScheduler scheduler;

        private InteractTarget target;

        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            if (target == null) return;

            mover.MoveTo(target.transform.position);

            if (IsInRange())
            {
                mover.Cancel();
                InteractBehavior();
            }
        }

        public void Interact(InteractTarget interactTarget)
        {
            scheduler.StartAction(this, interactTarget.gameObject);
            target = interactTarget;
        }

        public void Cancel()
        {
            target.Cancel();
            target = null;
            StopInteract();
        }

        public bool CanInteract(InteractTarget interactTarget)
        {
            if (interactTarget.IsBottomless())
            {
                return Ingredient() != null;
            }

            IngredientStorage[] neededIngredients = interactTarget.NeededIngredients();
            if (neededIngredients.Length > 0 && !interactTarget.HaveAllIngredients())
            {
                return System.Array.Find(neededIngredients, neededIngredient => neededIngredient.ingredient == Ingredient()).ingredient != null;
            }
            else
            {
                return Ingredient() == null;
            }
        }

        private void InteractBehavior()
        {
            Vector3 interactPoint = target.transform.position + target.Direction();
            transform.LookAt(new Vector3(interactPoint.x, transform.position.y, interactPoint.z));
            TriggerInteract();

            if (!target.IsComplete())
            {
                target.StartInteraction(this);
            }
        }

        private bool IsInRange()
        {
            Transform targetTransform = target.transform;
            return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetTransform.position.x, 0, targetTransform.position.z)) <= range;
        }

        private void TriggerInteract()
        {
            animator.SetTrigger("interact");
            animator.ResetTrigger("stopInteract");
        }

        private void StopInteract()
        {
            animator.SetTrigger("stopInteract");
            animator.ResetTrigger("interact");
        }

        public Ingredient Ingredient()
        {
            return ingredient;
        }

        public void SetIngredient(Ingredient newValue)
        {
            ingredient = newValue;
        }
    }
}
