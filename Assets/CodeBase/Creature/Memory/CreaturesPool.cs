namespace PolygonGame.Creature.Memory
{
    using System.Collections.Generic;
    using PolygonGame.Creature.Spawn;
    using UnityEngine;

    public class CreaturesPool
    {
        private readonly Stack<GameObject> creatures;

        public CreaturesPool(int amount)
        {
            creatures = new Stack<GameObject>(amount);
            Populate(amount);
        }

        public GameObject Get()
        {
            if (creatures.Count == 0)
            {
                Populate(1);
            }

            return creatures.Pop();
        }

        public void Release(GameObject gameObject)
        {
            gameObject.SetActive(false);

            IPoolCleanable[] cleanables = gameObject.GetComponents<IPoolCleanable>();
            for (int i = 0; i < cleanables.Length; i++)
            {
                cleanables[i].Clean();
            }

            creatures.Push(gameObject);
        }

        public void Populate(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                creatures.Push(CreatureBuilder.BuildBlank());
            }
        }
    }
}