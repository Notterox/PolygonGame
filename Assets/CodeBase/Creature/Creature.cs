namespace PolygonGame.Creature
{
    using System;
    using PolygonGame.Creature.Behavior;
    using PolygonGame.Creature.Locator;
    using PolygonGame.Creature.Memory;
    using PolygonGame.Creature.StaticData;
    using UnityEngine;

    [RequireComponent(typeof(MeshRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
    public class Creature : MonoBehaviour, IPoolCleanable
    {
        private const int MaxHits = 4;
        private readonly int colorProperty = Shader.PropertyToID("_Color");

        private new Collider2D collider;
        private new Rigidbody2D rigidbody;
        private IBehavior behavior;
        private IDirectionsProvider allowedDirections;
        private ContactFilter2D borderContactFilter;
        private ICreatureLocator locator;

        public event Action<ConsumeEventArgs> Consume;

        public int Sides { get; set; }

        public ICreatureLocator Locator { get => locator; set => UpdateLocator(value); }

        public bool Move(Vector2 movement)
        {
            (Vector2 pos, bool hitSomething) = CalculateNextPosition(movement);

            locator?.UpdateLocation(gameObject);
            rigidbody.MovePosition(pos);

            return hitSomething;
        }

        public void LookAt(Vector2 point)
        {
            Vector2 direction = (point - (Vector2)transform.position).normalized;
            rigidbody.SetRotation(Vector2.SignedAngle(Vector2.up, direction));
        }

        public Vector2 RandomDirection()
        {
            if (allowedDirections == null)
            {
                return Vector2.zero;
            }

            return transform.localToWorldMatrix
                .MultiplyVector(allowedDirections.RandomDirection(Sides));
        }

        public Vector2 RandomDirectionAwayFrom(Vector2 point)
        {
            Vector2 directionToPoint = point - (Vector2)transform.position;
            Vector2 newDirection = RandomDirection();

            while (IsDirectedAway(directionToPoint, newDirection))
            {
                newDirection = RandomDirection();
            }

            return newDirection;
        }

        public void SetBehavior(IBehavior behavior)
        {
            this.behavior = behavior;
            behavior?.Init(this);
        }

        public void SetAllowedDirections(IDirectionsProvider directions)
        {
            allowedDirections = directions;
        }

        public virtual void SetSides(int sides)
        {
            Sides = sides;
        }

        public void SetColor(Color color)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor(colorProperty, color);

            renderer.SetPropertyBlock(propertyBlock);
        }

        public void Clean()
        {
            Consume = null;
            behavior = null;
            allowedDirections = null;
        }

        private void OnEnable()
        {
            collider = GetComponent<Collider2D>();
            rigidbody = GetComponent<Rigidbody2D>();
            borderContactFilter = default;
            borderContactFilter.SetLayerMask(Defaults.GetInstance().BorderMask);

            Locator?.Add(gameObject);
            behavior?.Init(this);
        }

        private void OnDisable()
        {
            Locator?.Remove(gameObject);
        }

        private void FixedUpdate()
        {
            behavior?.Update(this, Time.fixedDeltaTime);
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            Creature otherCreature = col.gameObject.GetComponent<Creature>();
            if (otherCreature != null && col.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                if (GetInstanceID() < otherCreature.GetInstanceID())
                {
                    Consume?.Invoke(new ConsumeEventArgs()
                    {
                        Victim = otherCreature,
                        Consumer = this,
                        ContactPoint = col.contacts[0].point,
                    });
                }
            }
        }

        private (Vector2 pos, bool hitSomething) CalculateNextPosition(Vector2 movement)
        {
            RaycastHit2D[] hits = new RaycastHit2D[4];
            int hitsCount = collider.Cast(movement.normalized, borderContactFilter, hits, movement.magnitude);

            if (hitsCount > 0)
            {
                Vector2 avgNormal = AverageNormal(hits, hitsCount);
                Vector2 avgCentroid = AverageCentroid(hits, hitsCount);

                if (Vector2.Dot(movement.normalized, avgNormal) < 0)
                {
                    return (avgCentroid, true);
                }
            }

            return (rigidbody.position + movement, false);
        }

        private bool IsDirectedAway(Vector2 lhs, Vector2 rhs) => Vector2.Dot(lhs, rhs) > 0;

        private Vector2 AverageCentroid(RaycastHit2D[] hits, int hitsCount)
        {
            Vector2 sum = Vector2.zero;
            for (int i = 0; i < hitsCount; i++)
            {
                sum += hits[i].centroid;
            }

            return sum / hitsCount;
        }

        private Vector2 AverageNormal(RaycastHit2D[] hits, int hitsCount)
        {
            Vector2 sum = Vector2.zero;
            for (int i = 0; i < hitsCount; i++)
            {
                sum += hits[i].normal;
            }

            return sum / hitsCount;
        }

        private void UpdateLocator(ICreatureLocator locator)
        {
            if (Locator != null)
            {
                this.locator.Remove(gameObject);
            }

            locator.Add(gameObject);
            this.locator = locator;
        }
    }
}