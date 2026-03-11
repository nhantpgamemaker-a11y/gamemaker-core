using TMPro;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class UIVersion : MonoBehaviour
    {   
        [SerializeField] private TMP_Text _txtVersion;
        public void Start()
        {
            _txtVersion.text = $"{Application.version}";
        }
    }
}
