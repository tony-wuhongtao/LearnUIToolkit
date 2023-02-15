using System;
using System.Collections;
using TonyDev.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace TonyDev.UI
{
    public class TransitionScreen : MonoBehaviour
    {
        private const string UssFade = "fade";
        private VisualElement transitionImage;
        private WaitUntil waitUntilSceneHasLoading;
        public static event System.Action ShowLoadingScreen;

        private void Awake()
        {
            transitionImage = GetComponent<UIDocument>().rootVisualElement.Q("TransitionImage");
            waitUntilSceneHasLoading = new WaitUntil(() => SceneLoader.IsSceneLoaded);

            SceneLoader.LoadingStarted += FadeOut;
            SceneLoader.LoadingCompleted += FadeIn;
            
        }

        IEnumerator ActivateLoadedSceneCoroutine()
        {
            yield return waitUntilSceneHasLoading;
            
            SceneLoader.ActivateLoadedScene();
        }
        
        private void FadeOut()
        {
            transitionImage.AddToClassList(UssFade);
            transitionImage.RegisterCallback<TransitionEndEvent>(OnFadeOutEnded);
            
        }

        private void FadeIn()
        {
            transitionImage.RemoveFromClassList(UssFade);
        }

        private void OnFadeOutEnded(TransitionEndEvent evt)
        {
            transitionImage.UnregisterCallback<TransitionEndEvent>(OnFadeOutEnded);
            if (SceneLoader.ShowLoadingScreen)
            {
                // trigger show loading screen event
                ShowLoadingScreen?.Invoke();
            }
            else
            {
                StartCoroutine(ActivateLoadedSceneCoroutine());
            }
            
        }

        
    }
}
