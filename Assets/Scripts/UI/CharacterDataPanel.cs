using System.Collections;
using System.Collections.Generic;
using TonyDev.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace TonyDev.UI
{
    public class CharacterDataPanel: VisualElement
    {
        private readonly TemplateContainer templateContainer;
        readonly List<VisualElement> stateContainers;
        public new class UxmlFactory: UxmlFactory<CharacterDataPanel>{}

        public CharacterDataPanel()
        {
            templateContainer = Resources.Load<VisualTreeAsset>("CharacterDataPanel").Instantiate();
            templateContainer.style.flexGrow = 1.0f;
            hierarchy.Add(templateContainer);
        }

        public CharacterDataPanel(CharacterData characterData) : this()
        {
            userData = characterData;

            templateContainer.Q("Avatar").style.backgroundImage = characterData.CharacterAvatarImage;
            templateContainer.Q<Label>("NameLabel").text = characterData.CharacterName;

            stateContainers = templateContainer.Query("StateContainer").ToList();

            UpdateCharacterState();

            templateContainer.RegisterCallback<MouseDownEvent>(OnCliked);

            // Clickable leftClickManipulator = new Clickable(OnLeftClicked);
            // leftClickManipulator.activators.Clear();
            // // leftClickManipulator.activators.Add(new ManipulatorActivationFilter(){ button = MouseButton.LeftMouse,clickCount = 1,modifiers = EventModifiers.Shift});
            // leftClickManipulator.activators.Add(new ManipulatorActivationFilter(){ button = MouseButton.LeftMouse});
            //
            // templateContainer.AddManipulator(leftClickManipulator);
            //
            // Clickable rightClickManipulator = new Clickable(OnRightClicked);
            // rightClickManipulator.activators.Clear();
            // rightClickManipulator.activators.Add(new ManipulatorActivationFilter(){ button = MouseButton.RightMouse});
            //
            // templateContainer.AddManipulator(rightClickManipulator);
        }

        private void OnCliked(MouseDownEvent evt)
        {
            var characterData = (CharacterData)userData;
            if (evt.button == 0)
            {
                characterData.CharacterLevel++;
            }

            if (evt.button == 1)
            {
                characterData.CharacterLevel--;
            }
            UpdateCharacterState();
        }

        // private void OnRightClicked(EventBase obj)
        // {
        //     ((CharacterData)userData).CharacterLevel--;
        //     UpdateCharacterState();
        // }
        //
        // private void OnLeftClicked(EventBase obj)
        // {
        //     ((CharacterData)userData).CharacterLevel++;
        //     UpdateCharacterState();
        // }

        private void UpdateCharacterState()
        {
            var characterData = (CharacterData)userData;

            SetCharacterState(stateContainers[0],"等级",characterData.CharacterLevel);
            SetCharacterState(stateContainers[1],"行动力",characterData.CharacterStates.initiative);
            SetCharacterState(stateContainers[2],"最大HP",characterData.CharacterStates.maxHp);
            SetCharacterState(stateContainers[3],"最大MP",characterData.CharacterStates.maxMp);
            SetCharacterState(stateContainers[4],"攻击力",characterData.CharacterStates.attack);
            SetCharacterState(stateContainers[5],"防御力",characterData.CharacterStates.defense);
        }

        private void SetCharacterState(VisualElement stateContainer, string titleText, int value)
        {
            stateContainer.Q<Label>("StateTitleLabel").text = titleText;
            stateContainer.Q<Label>("StateValueLabel").text = value.ToString();
        }
    }
}

