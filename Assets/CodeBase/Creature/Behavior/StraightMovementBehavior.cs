namespace PolygonGame.Creature.Behavior
{
    using PolygonGame.Creature.StaticData;
    using UnityEngine;

    public class StraightMovementBehavior : IBehavior
    {
        private Vector2 currentDirection;

        private float speed;

        public static StraightMovementBehavior Create() => new StraightMovementBehavior();

        public void Init(Creature creature)
        {
            speed = Defaults.GetInstance().Creature.StraightBehavior.Speed;
            currentDirection = creature.RandomDirection();
            creature.SetColor(Defaults.GetInstance().Creature.StraightBehavior.Color);
        }

        public void Update(Creature creature, float time)
        {
            if (creature.Move(speed * time * currentDirection))
            {
                currentDirection = creature.RandomDirection();
            }
        }
    }
}