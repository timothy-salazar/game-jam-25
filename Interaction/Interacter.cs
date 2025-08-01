using UnityEngine;
using GJ2025.Core;
using GJ2025.Movement;

namespace GJ2025.Interaction
{
    public class Interacter : MonoBehaviour, IAction
    {
        private Mover mover;
        private Animator animator;
        private ActionScheduler scheduler;

        private Transform target;
        [SerializeField] float range = 1f;

        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            scheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            if (target != null)
            {
                mover.MoveTo(target.position);

                if (IsInRange())
                {
                    mover.Cancel();
                    InteractBehavior();
                }
            }
        }

        public void Interact(InteractTarget interactTarget)
        {
            scheduler.StartAction(this);
            target = interactTarget.transform;
        }

        public void Cancel()
        {
            target.GetComponent<MeshRenderer>().enabled = true;
            target = null;
            StopInteract();
        }

        private bool IsInRange()
        {
            return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z)) <= range;
        }

        private void InteractBehavior()
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            target.GetComponent<MeshRenderer>().enabled = false;
            TriggerInteract();
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
