using UnityEngine;
using GJ2025.Core;
using GJ2025.Movement;
using Unity.VisualScripting;
using NUnit.Framework;

namespace GJ2025.Interaction
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(ActionScheduler))]

    public class Interacter : MonoBehaviour, IAction
    {
        private Mover mover;
        private Animator animator;
        private ActionScheduler scheduler;

        private InteractTarget target;
        [SerializeField] float range = 3f;

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
            scheduler.StartAction(this);
            target = interactTarget;
        }

        public void Cancel()
        {
            target.GetComponent<MeshRenderer>().enabled = true;
            target.Cancel();
            target = null;
            StopInteract();
        }

        private void InteractBehavior()
        {
            Vector3 interactPoint = target.transform.position + target.Direction();
            transform.LookAt(new Vector3(interactPoint.x, transform.position.y, interactPoint.z));
            target.GetComponent<MeshRenderer>().enabled = false;
            TriggerInteract();

            if (target.IsComplete())
            {
                scheduler.StartAction(null);
            }
            else
            {
                target.StartTimer();
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
    }
}
