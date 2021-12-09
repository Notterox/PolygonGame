namespace PolygonGame.Creature.Behavior
{
    public interface IBehavior
    {
        void Init(Creature creature);

        void Update(Creature creature, float time);
    }
}
