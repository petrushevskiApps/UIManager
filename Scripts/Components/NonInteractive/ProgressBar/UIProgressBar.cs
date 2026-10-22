using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TwoOneTwoGames.UIManager.Components.NonInteractive
{
    public class UIProgressBar : MonoBehaviour
    {
        [Tooltip("Optional. When set, the bar drives this Slider. When empty, it drives the fill image's fill amount.")]
        [SerializeField]
        private Slider _slider;

        [Tooltip("Takes the bar's color. Without a Slider it must be a Filled image, whose fill method and origin set the bar's direction.")]
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

        public void SetData(UIProgressBarData progressBarData)
        {
            if (!progressBarData.IsVisible)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            UpdateProgress(progressBarData.Minimum, progressBarData.Maximum, progressBarData.Current);

            if (progressBarData.Color.HasValue) SetColor(progressBarData.Color.Value);
        }

        private void UpdateProgress(float minimum, float maximum, float current)
        {
            // Range can be zero-width, which would make the fraction undefined
            var range = maximum - minimum;
            var fraction = range > 0f ? Mathf.Clamp01((current - minimum) / range) : 0f;

            if (_slider != null)
            {
                _slider.minValue = minimum;
                _slider.maxValue = maximum;
                _slider.value = current;
            }
            else
            {
                _fillImage.fillAmount = fraction;
            }

            UpdateText(fraction);
        }

        private void SetColor(Color color)
        {
            if (_fillImage == null) return;

            _fillImage.color = color;
        }

        private void UpdateText(float fraction)
        {
            if (_text == null) return;

            ApplyOutline();

            _text.text = $"{Mathf.RoundToInt(fraction * 100f)}%";
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