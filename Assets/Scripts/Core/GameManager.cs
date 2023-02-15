using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TonyDev.Core
{
    public class GameManager : MonoBehaviour
    {
        private const string GameManagerKey = "GameManager";
        // private void Awake()
        // {
        //     DontDestroyOnLoad(gameObject);
        // }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InstantiateGameManager()
        {
            //Runtime Assets Loading
            //Method 1. Resources Folder , move prefabs to prefabs/Resources and
            // Resources.Load<GameObject>("GameManager");
            //Method 2. Addressable Assets
            // Addressables.InstantiateAsync(GameManagerKey).Completed += OnInstantiated;
            Addressables.InstantiateAsync(GameManagerKey).Completed += operationHandle =>
            {
                DontDestroyOnLoad(operationHandle.Result);
            };
        }

        // private static void OnInstantiated(AsyncOperationHandle<GameObject> obj)
        // {
        //     DontDestroyOnLoad(obj.Result);
        // }
    }
}
