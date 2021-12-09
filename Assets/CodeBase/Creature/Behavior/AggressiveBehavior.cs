namespace PolygonGame.Creature.Behavior
{
    using System.Collections;
    using PolygonGame.Creature.StaticData;
    using UnityEngine;

    public class AggressiveBehavior : IBehavior
    {
        private GameObject target;

        private float nextTargetChangeTime;
        private float speed;
        private float changeTargetTime;

        public static AggressiveBehavior Create() => new AggressiveBehavior();

        public void Init(Creature creature)
        {
            target = null;
            nextTargetChangeTime = 0.0f;
            speed = Defaults.GetInstance().Creature.AggressiveBehavior.Speed;
            changeTargetTime = Defaults.GetInstance().Creature.AggressiveBehavior.ChangeTargetTime;
            creature.SetColor(Color.red);
        }

        public void Update(Creature creature, float time)
        {
            if (!HasTarget() || Time.time > nextTargetChangeTime)
            {
                SelectTarget(creature);
                nextTargetChangeTime = Time.time + changeTargetTime;
            }

            if (!HasTarget())
            {
                return;
            }

            Transform transform = creature.gameObject.transform;

            Vector2 direction = transform.localToWorldMatrix.MultiplyVector(Vector2.up);
            creature.LookAt(target.transform.position);
            creature.Move(speed * time * direction);
        }

        private bool HasTarget()
        {
            return target != null && target.activeSelf;
        }

        private void SelectTarget(Creature creature)
        {
            target = creature.Locator?.GetClosest(creature.gameObject);
        }
    }
}