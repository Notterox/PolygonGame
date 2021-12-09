namespace PolygonGame.Creature.Locator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class CreatureLocatorSimple : ICreatureLocator
    {
        private readonly HashSet<GameObject> creatures;

        public CreatureLocatorSimple()
        {
            creatures = new HashSet<GameObject>();
        }

        public void Add(GameObject creature) => creatures.Add(creature);

        public bool Remove(GameObject creature) => creatures.Remove(creature);

        public GameObject GetClosest(GameObject creature)
        {
            GameObject closest = null;
            float closestSqrMagnitude = float.MaxValue;

            foreach (GameObject other in creatures)
            {
                if (other == creature)
                {
                    continue;
                }

                float otherSqrMagnitude = (other.transform.position - creature.transform.position).sqrMagnitude;
                if (otherSqrMagnitude < closestSqrMagnitude)
                {
                    closest = other;
                    closestSqrMagnitude = otherSqrMagnitude;
                }
            }

            return closest;
        }

        public void UpdateLocation(GameObject creature)
        {
            // This implementation so simple that it doesn't need that
        }

        public void Clear()
        {
            creatures.Clear();
        }
    }
}