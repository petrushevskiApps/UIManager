using System;
using System.Collections;
using System.Collections.Generic;
using TwoOneTwoGames.UIManager.Data;
using TwoOneTwoGames.UIManager.Interfaces;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.Components.NonInteractive
{
    public class UIStars : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _stars;

        [SerializeField]
        [Tooltip("Delay the start of the Stars fill up in seconds.")]
        private float _delayPresentationStart = 1f;

        [SerializeField]
        [Tooltip("Should SFX be played when the star is filled.")]
        private bool _playSfx = true;

        [SerializeField]
        private float _delayBetweenStars = 0.4f;
        
        // Injected
        private IUiAudioPalette _uiAudioPalette;
        private IUiSoundSystem _uiSoundSystem;

        [Inject]
        public void Setup(
            IUiSoundSystem uiSoundSystem,
            IUiAudioPalette uiAudioPalette)
        {
            _uiSoundSystem = uiSoundSystem;
            _uiAudioPalette = uiAudioPalette;
        }

        private void Awake()
        {
            ClearStars();
        }

        private void OnDisable()
        {
            ClearStars();
        }

        public void SetData(int starsCount)
        {
            gameObject.SetActive(true);
            StartCoroutine(DelayInvoke(() =>
            {
                for (var i = 0; i < starsCount; i++)
                {
                    var index = i;
                    StartCoroutine(DelayInvoke(() =>
                    {
                        _stars[index].SetActive(true);
                        if (_playSfx)
                        {
                            _uiSoundSystem?.PlayUiSoundEffect(_uiAudioPalette.StarShown);
                        }
                    },  _delayBetweenStars * index));
                }
            }, _delayPresentationStart));
            
        }

        private IEnumerator DelayInvoke(Action action, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke();
        }

        private void ClearStars()
        {
            _stars.ForEach(star => star.SetActive(false));
        }
    }
}