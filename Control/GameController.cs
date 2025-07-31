using UnityEngine;
using GJ2025.Core;
using GJ2025.Movement;

namespace GJ2025.Control
{
    public class GameController : MonoBehaviour
    {
        private Mover mover;
        [SerializeField] UnitController selectedUnit;

        void Start()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if (Select()) return;
            if (selectedUnit == null) return;
            if (selectedUnit.Interact()) return;
            if (selectedUnit.Move()) return;
        }

        private bool Select()
        {
            RaycastHit[] hits = Physics.RaycastAll(Helpers.GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                UnitController unit = hit.transform.GetComponent<UnitController>();
                if (unit != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        selectedUnit = unit;
                        unit.SetIsSelected(true);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
