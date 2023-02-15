using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = ("Data/CharacterStatesData"), fileName = ("CharacterStatesData_"))]
    public class CharacterStatesData : ScriptableObject
    {
        [SerializeField] private TextAsset dataFile;
        [SerializeField] private List<CharacterStates> characterStatesList;

        private void OnValidate()
        {
            if(!dataFile) return;
            
            characterStatesList.Clear();

            string[] textInLines = dataFile.text.Split("\n");
            foreach (string textInLine in textInLines)
            {
                for (int lineIndex = 1; lineIndex < textInLines.Length; lineIndex++)
                {
                    string[] statesValues = textInLines[lineIndex].Split(",");
                    CharacterStates currentLevelStates = new CharacterStates();

                    currentLevelStates.initiative = int.Parse(statesValues[0]);
                    currentLevelStates.maxHp = int.Parse(statesValues[1]);
                    currentLevelStates.maxMp = int.Parse(statesValues[2]);
                    currentLevelStates.attack = int.Parse(statesValues[3]);
                    currentLevelStates.defense = int.Parse(statesValues[4]);
                    
                    characterStatesList.Add(currentLevelStates);
                }
            }

        }

        public CharacterStates GetCharacterStateByLevel(int characterLevel)
        {
            return characterStatesList[characterLevel - 1];
        }
    }
}

