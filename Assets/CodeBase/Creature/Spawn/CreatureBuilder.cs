namespace PolygonGame.Creature.Spawn
{
    using PolygonGame.Creature.Behavior;
    using PolygonGame.Creature.Locator;
    using PolygonGame.Creature.StaticData;
    using PolygonGame.Services;
    using UnityEngine;

    public class CreatureBuilder
    {
        private const float FullCircle = 360f;
        private const int StickSides = 2;

        private readonly int sides;
        private readonly IMeshGenerator meshService;
        private readonly GameObject gameObject;
        private readonly Creature creature;

        public CreatureBuilder(int sides, IMeshGenerator meshService)
            : this(BuildBlank(), sides, meshService)
        {
        }

        public CreatureBuilder(GameObject blank, int sides, IMeshGenerator meshService)
        {
            this.sides = sides;
            this.meshService = meshService;
            this.gameObject = blank;
            gameObject.GetComponent<MeshFilter>().mesh = this.meshService.GetMesh(sides);
            gameObject.GetComponent<MeshRenderer>().material = Defaults.GetInstance().Creature.Material;

            creature = gameObject.GetComponent<Creature>();
            creature.SetSides(sides);
            SetupDirections();
            SetupColliders(creature);

            gameObject.SetActive(true);
        }

        public static GameObject BuildBlank()
        {
            GameObject blankCreature = new GameObject(
                "Creature",
                typeof(MeshFilter),
                typeof(MeshRenderer),
                typeof(Rigidbody2D),
                typeof(PolygonCollider2D),
                typeof(Creature));

            blankCreature.SetActive(false);

            SetupPhysics(blankCreature);

            return blankCreature;
        }

        public CreatureBuilder SetBehavior(IBehavior behavior)
        {
            creature.SetBehavior(behavior);
            return this;
        }

        public CreatureBuilder SetLocator(ICreatureLocator locator)
        {
            creature.Locator = locator;
            return this;
        }

        public CreatureBuilder PlaceAt(Vector2 point)
        {
            gameObject.transform.position = point;
            return this;
        }

        public CreatureBuilder RotateRandomly()
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.value * FullCircle);
            return this;
        }

        public Creature GetCreature() => creature;

        private static void SetupPhysics(GameObject blankCreature)
        {
            Rigidbody2D rigidbody = blankCreature.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;
            rigidbody.useFullKinematicContacts = true;
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private void SetupColliders(Creature creature)
        {
            PolygonCollider2D collider = gameObject.GetComponent<PolygonCollider2D>();
            creature.SetSides(sides);
            collider.points = meshService.GetPath(sides);
        }

        private void SetupDirections()
        {
            if (sides == StickSides)
            {
                creature.SetAllowedDirections(StickCreatureDirections.Instance);
                return;
            }

            creature.SetAllowedDirections(RegularCreatureDirections.Instance);
        }
    }
}