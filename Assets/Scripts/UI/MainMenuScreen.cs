using System;
using TonyDev.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace TonyDev.UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private AssetReference newGameScene;
        [SerializeField] private bool debugMode = true;
        private Button newGameButton;

        private void Awake()
        {
            VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            newGameButton = rootVisualElement.Q<Button>("ButtonNewGame");
        }

        private void OnEnable()
        {
            newGameButton.clicked += StartGame;
        }

        private void OnDisable()
        {
            newGameButton.clicked -= StartGame;
        }

        private void StartGame()
        {
            
            if (debugMode)
            {
                SceneLoader.LoadAddressableScene(SceneLoader.DebugRoomSceneKey);
            }
            else
            {
                //TODO: disable all player's inputs
                // Game settings initialization
                // Load default new game scene
                SceneLoader.LoadAddressableScene(newGameScene);
            }
        }
    }
}
