using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PartyDataScreen : MonoBehaviour
    {
        [SerializeField] private PartyData partyData;
        private VisualElement rootVisualElement;
        void Awake()
        {
            rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            var bodyContainer = rootVisualElement.Q("BodyContainer");
            bodyContainer.Clear();

            foreach (CharacterData characterData in partyData.CharacterDataList)
            {
                var characterDataPanel = new CharacterDataPanel(characterData);
                characterDataPanel.style.flexBasis = Length.Percent(25.0f);
                bodyContainer.Add(characterDataPanel);
            }
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rootVisualElement.style.display =
                    rootVisualElement.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
            }
            
        }
    }
}

