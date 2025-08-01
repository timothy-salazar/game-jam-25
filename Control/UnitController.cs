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
        [SerializeField] bool isSelected;

        private Interacter interacter;
        private Mover mover;

        void Start()
        {
            mover = GetComponent<Mover>();
            interacter = GetComponent<Interacter>();
        }

        void Update()
        {

        }

        public bool Interact()
        {
            RaycastHit[] hits = Physics.RaycastAll(Helpers.GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                InteractTarget target = hit.transform.GetComponent<InteractTarget>();

                if (target != null)
                {
                    if (Input.GetMouseButtonDown(0))
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
                    if (Input.GetMouseButton(0))
                    {
                        mover.StartMoveAction(hit.point);
                    }

                    return true;
                }
            }

            return false;
        }

        public bool GetIsSelected()
        {
            return isSelected;
        }

        public void SetIsSelected(bool newValue)
        {
            isSelected = newValue;
        }
    }
}
