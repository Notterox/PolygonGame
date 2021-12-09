namespace PolygonGame.Creature.Locator
{
    using UnityEngine;

    public interface ICreatureLocator
    {
        GameObject GetClosest(GameObject creature);

        void Add(GameObject creature);

        bool Remove(GameObject creature);

        void UpdateLocation(GameObject creature);

        void Clear();
    }
}