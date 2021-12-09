namespace PolygonGame
{
    using System;
    using PolygonGame.Creature;
    using PolygonGame.Creature.Behavior;

    public class GameParameters
    {
        public GameParameters()
        {
            CreaturesAmount = 200;
            GameArea = 15f;
            MaxSides = 8;
            BehaviorSelector = new RandomSelector<Func<IBehavior>>()
                .Set(PassiveBehavior.Create, 0.25f)
                .Set(StraightMovementBehavior.Create, 0.25f)
                .Set(RandomMovementBehavior.Create, 0.25f)
                .Set(AggressiveBehavior.Create, 0.25f);
        }

        public event Action Updated;

        public int CreaturesAmount { get; private set; }

        public float GameArea { get; private set; }

        public int MaxSides { get; private set; }

        public RandomSelector<Func<IBehavior>> BehaviorSelector { get; private set; }

        public void SetCreaturesAmount(int amount)
        {
            CreaturesAmount = amount;
            Updated?.Invoke();
        }

        public void SetGameArea(float area)
        {
            GameArea = area;
            Updated?.Invoke();
        }

        public void SetMaxSides(int sides)
        {
            MaxSides = sides;
            Updated?.Invoke();
        }
    }
}