namespace PolygonGame.UI
{
    using System;
    using System.Globalization;
    using PolygonGame.Creature.Behavior;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIGameController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Game game;

        [Header("UI - Screens")]
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject inGame;

        [Header("UI - Probabilities Form")]
        [SerializeField] private NumberField probabilityPassiveField;
        [SerializeField] private NumberField probabilityStraightField;
        [SerializeField] private NumberField probabilityRandomField;
        [SerializeField] private NumberField probabilityAggressiveField;
        [SerializeField] private Button normalizeButton;

        [Header("UI - Probabilities Form")]
        [SerializeField] private NumberField creaturesAmountField;
        [SerializeField] private NumberField gameAreaSizeField;
        [SerializeField] private NumberField maxSidesField;

        [SerializeField] private Button startGameButton;
        [SerializeField] private Button resetButton;

        private void Awake()
        {
            ListenProbabilitiesChange();
            ListenParametersChange();

            startGameButton.onClick.AddListener(game.StartGame);
            resetButton.onClick.AddListener(game.ResetGame);
            game.Started += () =>
            {
                menu.SetActive(false);
                inGame.SetActive(true);
            };
            game.Reset += () =>
            {
                inGame.SetActive(false);
                menu.SetActive(true);
            };

            UpdateData();
        }

        private void ListenProbabilitiesChange()
        {
            probabilityPassiveField.OnChange += HandleProbabilityChange(PassiveBehavior.Create);
            probabilityStraightField.OnChange += HandleProbabilityChange(StraightMovementBehavior.Create);
            probabilityRandomField.OnChange += HandleProbabilityChange(RandomMovementBehavior.Create);
            probabilityAggressiveField.OnChange += HandleProbabilityChange(AggressiveBehavior.Create);
            normalizeButton.onClick.AddListener(() => game.GameParameters.BehaviorSelector.Normalize());

            game.GameParameters.BehaviorSelector.Updated += UpdateProbabilities;
        }

        private void ListenParametersChange()
        {
            creaturesAmountField.OnChange += ParseInt(game.GameParameters.SetCreaturesAmount);
            gameAreaSizeField.OnChange += ParseFloat(game.GameParameters.SetGameArea);
            maxSidesField.OnChange += ParseInt(game.GameParameters.SetMaxSides);

            game.GameParameters.Updated += UpdateParameters;
        }

        private void UpdateData()
        {
            UpdateProbabilities();
            UpdateParameters();
        }

        private void UpdateProbabilities()
        {
            var probabilities = game.GameParameters.BehaviorSelector;
            probabilityPassiveField.SetValue(probabilities.Get(PassiveBehavior.Create));
            probabilityStraightField.SetValue(probabilities.Get(StraightMovementBehavior.Create));
            probabilityRandomField.SetValue(probabilities.Get(RandomMovementBehavior.Create));
            probabilityAggressiveField.SetValue(probabilities.Get(AggressiveBehavior.Create));
        }

        private void UpdateParameters()
        {
            creaturesAmountField.SetValue(game.GameParameters.CreaturesAmount);
            gameAreaSizeField.SetValue(game.GameParameters.GameArea);
            maxSidesField.SetValue(game.GameParameters.MaxSides);
        }

        private Action<string> HandleProbabilityChange(Func<IBehavior> behaviorFactory)
        {
            return (string text) =>
            {
                if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                {
                    game.GameParameters.BehaviorSelector.Set(behaviorFactory, value);
                }
            };
        }

        private Action<string> ParseInt(Action<int> action) =>
            (string text) =>
            {
                if (int.TryParse(text, out int value))
                {
                    action(value);
                }
            };

        private Action<string> ParseFloat(Action<float> action) =>
            (string text) =>
            {
                if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                {
                    action(value);
                }
            };
    }
}