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
            animator.ResetTrigger("interact");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z)) <= range;
        }

        private void InteractBehavior()
        {
            target.GetComponent<MeshRenderer>().enabled = false;
            animator.SetTrigger("interact");
        }
    }
}
