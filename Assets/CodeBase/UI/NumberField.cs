namespace PolygonGame.UI
{
    using System;
    using System.Globalization;
    using TMPro;
    using UnityEngine;

    public enum NumberType
    {
        Float,
        Integer,
    }

    public class NumberField : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField input;
        [SerializeField]
        private NumberType numberType = NumberType.Float;
        [SerializeField]
        private float min;
        [SerializeField]
        private float max = 1.0f;

        private string oldText = string.Empty;

        private Func<string, string> validator;

        public event Action<string> OnChange;

        public void SetValue(float value)
        {
            input.text = value.ToString(CultureInfo.InvariantCulture);
            oldText = input.text;
        }

        public void SetValue(int value)
        {
            SetValue((float)value);
        }

        private void OnEnable()
        {
            input.onSelect.AddListener(HandleSelect);
            input.onDeselect.AddListener(HandleDeselect);

            if (numberType == NumberType.Integer)
            {
                validator = IntegerValidator;
            }
            else
            {
                validator = FloatValidator;
            }
        }

        private void HandleSelect(string text)
        {
            oldText = text;
        }

        private void HandleDeselect(string text)
        {
            string validated = validator(text);
            if (validated != oldText)
            {
                OnChange?.Invoke(validated);

                return;
            }

            input.text = oldText;
        }

        private string IntegerValidator(string newText) => IsInteger(newText, out int value)
            ? Clamp(value)
            : oldText;

        private string FloatValidator(string newText) => IsFloat(newText, out float value)
            ? Clamp(value)
            : oldText;

        private bool IsInteger(string text, out int value) => int.TryParse(text, out value);

        private bool IsFloat(string text, out float value) =>
            float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

        private string Clamp(float value) => Mathf.Clamp(value, min, max).ToString(CultureInfo.InvariantCulture);
    }
}