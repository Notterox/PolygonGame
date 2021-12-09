namespace PolygonGame.Creature.Behavior
{
    using PolygonGame.Creature.StaticData;
    using UnityEngine;

    public class RandomMovementBehavior : IBehavior
    {
        private float changeDirectionTime;
        private float speed;
        private Vector2 currentDirection;
        private float nextDirectionChangeTime;

        public static RandomMovementBehavior Create() => new RandomMovementBehavior();

        public void Init(Creature creature)
        {
            changeDirectionTime = Defaults.GetInstance().Creature.RandomBehavior.ChangeDirectionTime;
            speed = Defaults.GetInstance().Creature.RandomBehavior.Speed;

            currentDirection = Vector2.zero;
            nextDirectionChangeTime = 0.0f;

            creature.SetColor(Defaults.GetInstance().Creature.RandomBehavior.Color);
        }

        public void Update(Creature creature, float time)
        {
            if (Time.time > nextDirectionChangeTime)
            {
                currentDirection = creature.RandomDirection();
                nextDirectionChangeTime = Time.time + changeDirectionTime;
            }

            creature.Move(speed * time * currentDirection);
        }
    }
}