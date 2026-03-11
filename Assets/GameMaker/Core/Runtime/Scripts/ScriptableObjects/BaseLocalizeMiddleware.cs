using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    
    [CreateAssetMenu(fileName = "LocalizeMiddleware", menuName = "GameMaker/Core/Utils/LocalizeMiddleware", order = 0)]
    public class BaseLocalizeMiddleware : ScriptableObject
    {
        public Action OnLocalizeChanged;
        public virtual string LocalizeText(string key)
        {
            // Implement your localization logic here, for example, using a dictionary or an external localization system.
            // This is a placeholder implementation and should be replaced with actual localization logic.
            return key; // Return the localized text based on the key.
        }
        public virtual Sprite LocalizeIcon(string key)
        {
            // Implement your localization logic for icons here, for example, using a dictionary or an external localization system.
            // This is a placeholder implementation and should be replaced with actual localization logic.
            return null; // Return the localized icon based on the key.
        }
    }
}