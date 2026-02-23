using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    [CreateAssetMenu(fileName = "ChapterConfig", menuName ="GamePlay/ChapterConfig")]
    public class ChapterConfig : ScriptableObjectSingleton<ChapterConfig>
    {
        [UnityEngine.SerializeField]
        private List<ChapterDefinition> _chapterDefinitions;

        public ChapterDefinition GetChapter(string id)
        {
            return _chapterDefinitions.FirstOrDefault(x => x.GetID() == id);
        }
        public ChapterDefinition GetChapter(int level)
        {
            for (int i = 0; i < _chapterDefinitions.Count; i++)
            {
                if (level >= _chapterDefinitions[i].FromLevel  && level <= _chapterDefinitions[i].ToLevel)
                {
                    return _chapterDefinitions[i];
                }
            }
            return null;
        }
    }
    [System.Serializable]
    public class ChapterDefinition : IDefinition
    {
        [UnityEngine.SerializeField]
        private string _id;
        [UnityEngine.SerializeField]
        private string _name;
        [UnityEngine.SerializeField]
        private int _maxLevel;
        [UnityEngine.SerializeField]
        private List<string> _mapIds;
        
        [UnityEngine.SerializeField]
        private int _fromLevel;
        [UnityEngine.SerializeField]
        private int _toLevel;

        public int MaxLevel => _maxLevel;
        public List<string> MapIds => _mapIds;
        public int FromLevel { get => _fromLevel; }
        public int ToLevel { get => _toLevel; }

        public bool Equals(IDefinition other)
        {
            return other.GetID() == _id;
        }

        public string GetID()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

    }
}
