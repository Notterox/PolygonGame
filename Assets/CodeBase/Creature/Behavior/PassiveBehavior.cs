namespace PolygonGame.Creature.Behavior
{
    using PolygonGame.Creature.StaticData;

    public class PassiveBehavior : IBehavior
    {
        public static PassiveBehavior Create() => new PassiveBehavior();

        public void Init(Creature creature)
        {
            creature.SetColor(Defaults.GetInstance().Creature.PassiveBehavior.Color);
        }

        public void Update(Creature creature, float time)
        {
        }
    }
}