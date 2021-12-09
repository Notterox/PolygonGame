namespace PolygonGame.Creature
{
    using System.Collections.Generic;
    using PolygonGame.Creature.Locator;
    using PolygonGame.Creature.Memory;
    using PolygonGame.Creature.Spawn;
    using PolygonGame.Services;
    using UnityEngine;

    public class CreatureService : ICreatureService
    {
        private readonly IMeshGenerator meshGenerator;
        private readonly HashSet<Creature> creatures;
        private readonly CreaturesPool pool;
        private ICreatureLocator locator;
        private CreatureSpawner spawner;

        public CreatureService(IMeshGenerator meshGenerator)
        {
            this.meshGenerator = meshGenerator;
            this.pool = new CreaturesPool(250);
            locator = new CreatureLocatorSimple();
        }

        public void SpawnCreatures(SpawnParameters parameters)
        {
            if (parameters.ConsumeHandler == null)
            {
                parameters.ConsumeHandler = HandleConsumeEvent;
            }

            if (spawner == null)
            {
                spawner = new CreatureSpawner(parameters, pool, meshGenerator, locator);
            }

            spawner.SpawnAll();
        }

        public void ResetCreatures()
        {
            spawner.Reset();
            spawner = null;
        }

        private void HandleConsumeEvent(ConsumeEventArgs args)
        {
            int sidesSum = args.Victim.Sides + args.Consumer.Sides;

            DestroyCreature(args.Consumer);
            DestroyCreature(args.Victim);

            spawner.Spawn(sidesSum, args.ContactPoint);
        }

        private void DestroyCreature(Creature creature)
        {
            if (creature == null)
            {
                return;
            }

            spawner.Destroy(creature);
        }
    }
}