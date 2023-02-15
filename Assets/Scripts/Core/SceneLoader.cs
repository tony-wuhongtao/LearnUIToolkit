using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace TonyDev.Core
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader instance;
        private static SceneInstance loadedSceneInstance;
        
        public const string MainMenuSceneKey = "MainMenuScene";
        public const string DebugRoomSceneKey = "DebugRoomScene";

        public static event System.Action LoadingStarted;
        public static event System.Action<float> IsLoading;
        public static event System.Action LoadingSuccessed;
        public static event System.Action LoadingCompleted;
        
        public static bool ShowLoadingScreen { get; private set; }
        public static bool IsSceneLoaded { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        static IEnumerator LoadAddressableSceneCoroutine(object sceneKey, bool showLoadingScreen, bool loadSceneAdditively, bool activeOnLoad)
        {
            LoadSceneMode loadSceneMode = loadSceneAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single;
            var asyncOperationHandle = Addressables.LoadSceneAsync(sceneKey, loadSceneMode, activeOnLoad);
            LoadingStarted?.Invoke();
            ShowLoadingScreen = showLoadingScreen;

            while (asyncOperationHandle.Status != AsyncOperationStatus.Succeeded)
            {
                IsLoading?.Invoke(asyncOperationHandle.PercentComplete);
                yield return null;
            }

            if (activeOnLoad)
            {
                LoadingCompleted?.Invoke();
                yield break;
            }
            
            LoadingSuccessed?.Invoke();
            IsSceneLoaded = true;
            loadedSceneInstance = asyncOperationHandle.Result;


        }

        public static void ActivateLoadedScene()
        {
            loadedSceneInstance.ActivateAsync().completed += _ =>
            {
                IsSceneLoaded = false;
                loadedSceneInstance = default;
                LoadingCompleted?.Invoke();
            };
        }
        
        public static void LoadAddressableScene(
            object sceneKey, 
            bool showLoadingScreen = false, 
            bool loadSceneAdditively = false, 
            bool activeOnLoad = false)
        {
            // LoadSceneMode loadSceneMode = loadSceneAdditively ? LoadSceneMode.Additive : LoadSceneMode.Single;
            // Addressables.LoadSceneAsync(sceneKey, loadSceneMode, activeOnLoad);
            instance.StartCoroutine(LoadAddressableSceneCoroutine(sceneKey, showLoadingScreen, loadSceneAdditively, activeOnLoad));
        }
    }
}
