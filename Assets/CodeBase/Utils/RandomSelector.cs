namespace PolygonGame.Creature
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Random = UnityEngine.Random;

    public class RandomSelector<T>
    {
        private readonly Dictionary<T, float> probabilities;
        private float probabilitySum = 0.0f;
        private bool normalized = false;

        public RandomSelector()
        {
            probabilities = new Dictionary<T, float>();
        }

        public event Action Updated;

        public RandomSelector<T> Set(T item, float probability)
        {
            normalized = false;
            if (probabilities.TryGetValue(item, out float oldProbability))
            {
                probabilitySum -= oldProbability;
                probabilities[item] = probability;
            }
            else
            {
                probabilities.Add(item, probability);
            }

            probabilitySum += probability;

            Updated?.Invoke();

            return this;
        }

        public float Get(T item)
        {
            return probabilities[item];
        }

        public T Select(float randomValue)
        {
            Normalize();
            float cumulativeProbability = 0.0f;

            foreach (var pair in probabilities)
            {
                cumulativeProbability += pair.Value;
                if (randomValue <= cumulativeProbability)
                {
                    return pair.Key;
                }
            }

            return probabilities.Last().Key;
        }

        public T Select()
        {
            return Select(Random.value);
        }

        public RandomSelector<T> Normalize()
        {
            if (normalized || probabilities.Count == 0)
            {
                return this;
            }

            foreach (T key in new List<T>(probabilities.Keys))
            {
                probabilities[key] /= probabilitySum;
            }

            probabilitySum = 1.0f;
            normalized = true;

            Updated?.Invoke();

            return this;
        }
    }
}