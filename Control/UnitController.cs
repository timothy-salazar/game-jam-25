using UnityEngine;
using UnityEngine.AI;
using GJ2025.Core;
using GJ2025.Interaction;
using GJ2025.Movement;

namespace GJ2025.Control
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Interacter))]

    public class UnitController : MonoBehaviour
    {
        [SerializeField] bool isSelected = false;
        [SerializeField] bool isSelectable = false;

        private Interacter interacter;
        private Mover mover;

        private void OnEnable()
        {
            Helpers.OnUnitUnlock.AddListener(UnlockUnit);
        }

        void OnDisable()
        {
            Helpers.OnUnitUnlock.RemoveListener(UnlockUnit);
        }

        private void Start()
        {
            mover = GetComponent<Mover>();
            interacter = GetComponent<Interacter>();

        }

        public bool Interact()
        {
            RaycastHit[] hits = Physics.RaycastAll(Helpers.GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                InteractTarget target = hit.transform.GetComponent<InteractTarget>();

                if (target != null)
                {
                    if (interacter.CanInteract(target))
                    {
                        interacter.Interact(target);
                    }

                    return true;
                }
            }

            return false;
        }

        public bool Move()
        {
            RaycastHit[] hits = Physics.RaycastAll(Helpers.GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                TerrainCollider terrain = hit.transform.GetComponent<TerrainCollider>();
                if (terrain != null && NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, new NavMeshPath()))
                {
                    mover.StartMoveAction(hit.point);

                    return true;
                }
            }

            return false;
        }

        private void UnlockUnit(string unitTag)
        {
            if (tag == unitTag)
            {
                SetIsSelectable(true);
            }
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        public void SetIsSelected(bool newValue)
        {
            isSelected = newValue;
        }

        public bool IsSelectable()
        {
            return isSelectable;
        }

        public void SetIsSelectable(bool newValue)
        {
            isSelectable = newValue;
        }
    }
}
