using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TwoOneTwoGames.UIManager.Components.NonInteractive
{
    [RequireComponent(typeof(Slider))]
    public class UIProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        [SerializeField]
        private TextMeshProUGUI _text;

        [Tooltip("Outlines the percentage text so it stays readable where the fill passes behind it.")]
        [SerializeField]
        private bool _outlineText;

        [Tooltip("Usually the bar's background color, so the outline reads as a cut-out.")]
        [SerializeField]
        private Color _outlineColor = Color.black;

        [SerializeField, Range(0f, 1f)]
        private float _outlineWidth = 0.2f;

        private bool _outlineApplied;

        private float _currentProgress;
        private float _maxProgress;

        private Slider _slider;

        // SetData can arrive before Awake when a parent is still inactive, so the slider is fetched on demand
        private Slider Slider => _slider ??= GetComponent<Slider>();

        public void SetData(UIProgressBarData progressBarData)
        {
            if (!progressBarData.IsVisible)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            Slider.minValue = progressBarData.Minimum;
            Slider.maxValue = progressBarData.Maximum;
            UpdateProgress(progressBarData.Current);

            if (progressBarData.Color.HasValue) SetColor(progressBarData.Color.Value);
        }

        private void UpdateProgress(float currentProgress)
        {
            Slider.value = currentProgress;
            UpdateText();
        }

        private void SetColor(Color color)
        {
            _fillImage.color = color;
        }

        private void UpdateText()
        {
            if (_text == null) return;

            ApplyOutline();

            // Slider range can be zero-width, which would make the percentage undefined
            var range = Slider.maxValue - Slider.minValue;
            var percentage = range > 0f ? (Slider.value - Slider.minValue) / range * 100f : 0f;

            _text.text = $"{Mathf.RoundToInt(percentage)}%";
        }

        // Set on the text's own material instance so other labels sharing the font material keep theirs.
        // Done on first use rather than in Awake, since SetData can arrive before Awake.
        private void ApplyOutline()
        {
            if (!_outlineText || _outlineApplied) return;
            _outlineApplied = true;

            var material = _text.fontMaterial;
            material.SetColor(ShaderUtilities.ID_OutlineColor, _outlineColor);
            material.SetFloat(ShaderUtilities.ID_OutlineWidth, _outlineWidth);
            // The outline widens the glyphs, so the mesh padding must grow or it gets clipped
            _text.UpdateMeshPadding();
        }
    }
}