using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = ("Data/CharacterData"), fileName = ("CharacterData_"))]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] Texture2D characterAvatarImage;
        [SerializeField] string characterName;
        [SerializeField, Range(1,CharacterMaxLevel)] int characterStartLevel = 1;

        // [SerializeField] CharacterStates characterStates;

        [SerializeField] private CharacterStatesData characterStatesData;

        int characterLevel;
        private const int CharacterMaxLevel = 10;

        public Texture2D CharacterAvatarImage => characterAvatarImage;
        public string CharacterName => characterName;

        public int CharacterLevel
        {
            get => characterLevel;
            set
            {
                if(characterLevel == value || value is < 1 or > CharacterMaxLevel) return;
                characterLevel = value;
            }
        }
        // public CharacterStates CharacterStates => characterStates;

        public CharacterStates CharacterStates => characterStatesData.GetCharacterStateByLevel(characterLevel);

        private void OnEnable()
        {
            characterLevel = characterStartLevel;
        }
    }

}
