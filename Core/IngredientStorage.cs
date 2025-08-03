using System;
using GJ2025.Interaction;

namespace GJ2025.Core
{
    [Serializable]
    public struct IngredientStorage
    {
        public Ingredient ingredient;
        public bool haveIngredient;

        public IngredientStorage(Ingredient ingredient, bool haveIngredient)
        {
            this.ingredient = ingredient;
            this.haveIngredient = haveIngredient;
        }
    }
}

