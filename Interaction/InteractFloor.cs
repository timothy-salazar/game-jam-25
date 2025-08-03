using UnityEngine;
using UnityEngine.UI;
using GJ2025.Core;

namespace GJ2025.Interaction
{
    public class InteractFloor : MonoBehaviour, IInteract
    {
        [SerializeField] Image image;

        public void SetupIcon(Sprite icon, float yOffset)
        {
            image.sprite = icon;
            image.rectTransform.localPosition = new Vector3(0, yOffset, 0);
        }
    }
}
