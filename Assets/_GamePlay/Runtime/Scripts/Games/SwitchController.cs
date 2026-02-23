using System;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    public class SwitchController : MonoBehaviour
    {
        public event Action<bool> OnStatusChanged;
        private bool _status = false;
    }
}
