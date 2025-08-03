using UnityEngine;

namespace GJ2025.Interaction
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredients/Make New Ingredient")]

    public class Ingredient : ScriptableObject
    {
        [SerializeField] Sprite icon;
        [SerializeField] int score;
        [SerializeField] float yOffset = 0;
        [SerializeField] string unitTag;

        public GameObject Drop(GameObject ingredient, Vector3 position)
        {
            return Instantiate(ingredient, position, Quaternion.identity);
        }

        public Sprite Icon()
        {
            return icon;
        }

        public int Score()
        {
            return score;
        }

        public float YOffset()
        {
            return yOffset;
        }

        public string UnitTag()
        {
            return unitTag;
        }
    }
}
