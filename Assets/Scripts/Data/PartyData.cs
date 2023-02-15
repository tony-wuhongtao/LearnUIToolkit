using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TonyDev.Data
{
    [CreateAssetMenu(menuName = ("Data/PartyData"), fileName = ("PartyData_"))]
    public class PartyData : ScriptableObject
    {
        [SerializeField] private List<CharacterData> characterDataList;

        public List<CharacterData> CharacterDataList => characterDataList;
    }

}
