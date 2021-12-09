namespace PolygonGame
{
    using PolygonGame.Creature.Spawn;

    public interface ICreatureService
    {
        void ResetCreatures();

        void SpawnCreatures(SpawnParameters parameters);
    }
}
