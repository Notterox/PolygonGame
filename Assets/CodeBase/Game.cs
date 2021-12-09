namespace PolygonGame
{
    using System;
    using System.Collections.Generic;
    using PolygonGame.Creature;
    using PolygonGame.Creature.Spawn;
    using PolygonGame.Creature.StaticData;
    using PolygonGame.Services;
    using UnityEngine;

    public enum GameState
    {
        Initial,
        Started,
    }

    public class Game : MonoBehaviour
    {
        private Camera mainCamera;
        private Rect gameField;
        private IMeshGenerator meshGenerator;
        private ICreatureService creatureService;

        public event Action Started;

        public event Action Reset;

        public GameState State { get; private set; }

        public GameParameters GameParameters { get; private set; }

        public void StartGame()
        {
            if (State == GameState.Initial)
            {
                SpawnParameters spawnParameters = new SpawnParameters()
                {
                    Amount = GameParameters.CreaturesAmount,
                    MaxSides = GameParameters.MaxSides,
                    BehaviorSelector = GameParameters.BehaviorSelector,
                    SpawnArea = gameField,
                };
                creatureService.SpawnCreatures(spawnParameters);

                State = GameState.Started;
                Started?.Invoke();
            }
        }

        public void ResetGame()
        {
            if (State == GameState.Started)
            {
                creatureService.ResetCreatures();
                State = GameState.Initial;
                Reset?.Invoke();
            }
        }

        private void OnEnable()
        {
            Defaults.LoadDefaults();
            mainCamera = Camera.main;
            meshGenerator = new MeshGeneratingService();
            GameParameters = new GameParameters();
            GameParameters.Updated += UpdateParameters;
            creatureService = new CreatureService(meshGenerator);

            UpdateParameters();
        }

        private void UpdateParameters()
        {
            mainCamera.orthographicSize = GameParameters.GameArea;
            SetGameField();
        }

        private void SetGameField()
        {
            Vector2 topLeft = mainCamera.ViewportToWorldPoint(Vector2.zero);
            Vector2 bottomRight = mainCamera.ViewportToWorldPoint(Vector2.one);
            gameField = new Rect(topLeft, bottomRight - topLeft);

            UpdateGameAreaBorders();
        }

        private void UpdateGameAreaBorders()
        {
            EdgeCollider2D collider = GetComponent<EdgeCollider2D>();

            collider.SetPoints(new List<Vector2>
        {
            mainCamera.ViewportToWorldPoint(Vector2.zero),
            mainCamera.ViewportToWorldPoint(Vector2.right),
            mainCamera.ViewportToWorldPoint(Vector2.one),
            mainCamera.ViewportToWorldPoint(Vector2.up),
            mainCamera.ViewportToWorldPoint(Vector2.zero),
        });
        }
    }
}
