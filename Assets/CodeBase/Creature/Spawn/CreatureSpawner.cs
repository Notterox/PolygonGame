namespace PolygonGame.Creature.Spawn
{
    using System.Collections.Generic;
    using PolygonGame.Creature.Locator;
    using PolygonGame.Creature.Memory;
    using PolygonGame.Creature.StaticData;
    using PolygonGame.Services;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class CreatureSpawner
    {
        private const int MinSides = 2;
        private readonly CreaturesPool pool;
        private readonly IMeshGenerator meshGenerator;
        private readonly ICreatureLocator locator;
        private readonly HashSet<GameObject> creatures;

        private SpawnParameters parameters;
        private Rect spawnArea;

        public CreatureSpawner(
            SpawnParameters parameters,
            CreaturesPool pool,
            IMeshGenerator meshGenerator,
            ICreatureLocator locator)
        {
            this.parameters = parameters;
            this.pool = pool;
            this.meshGenerator = meshGenerator;
            this.locator = locator;
            this.spawnArea = PadRect(parameters.SpawnArea, Defaults.GetInstance().Creature.Size);

            creatures = new HashSet<GameObject>();
        }

        public bool IsSpawned { get; private set; }

        public void SpawnAll()
        {
            for (int i = 0; i < parameters.Amount; i++)
            {
                SpawnRandom();
            }

            IsSpawned = true;
        }

        public Creature Spawn(int sides, Vector2 location)
        {
            GameObject blank = pool.Get();

            Creature creature = new CreatureBuilder(blank, sides, meshGenerator)
                .PlaceAt(location)
                .RotateRandomly()
                .SetBehavior(parameters.BehaviorSelector.Select()())
                .SetLocator(locator)
                .GetCreature();

            creature.Consume += parameters.ConsumeHandler;
            creatures.Add(creature.gameObject);

            return creature;
        }

        public void SpawnRandom()
        {
            int sides = Random.Range(MinSides, parameters.MaxSides + 1);
            Vector2 randomLocation = new Vector2(
                Random.Range(spawnArea.xMin, spawnArea.xMax),
                Random.Range(spawnArea.yMax, spawnArea.yMin));

            Spawn(sides, randomLocation);
        }

        public void Reset()
        {
            foreach (var creature in creatures)
            {
                pool.Release(creature);
            }

            IsSpawned = false;
            creatures.Clear();
        }

        public void Destroy(Creature creature)
        {
            if (creatures.Remove(creature.gameObject))
            {
                pool.Release(creature.gameObject);
            }
        }

        public IReadOnlyCollection<GameObject> GetCreatures()
        {
            return creatures;
        }

        private Rect PadRect(Rect rect, float padding) =>
            new Rect()
            {
                xMin = rect.xMin + padding,
                yMin = rect.yMin + padding,
                xMax = rect.xMax - padding,
                yMax = rect.yMax - padding,
            };
    }
}