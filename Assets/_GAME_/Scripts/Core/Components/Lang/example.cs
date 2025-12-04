using Game.System.Core;
using System;
using UnityEditor.Experimental;
using UnityEditor.Localization.Editor;
using UnityEngine;

namespace Game.System.Core
{
    /// <summary>
    /// Example usage of localization system
    /// </summary>
    public class LocalizationExample : Sys_Components
    {
        protected override void Init()
        {
            // Get simple localized text
            string welcomeText = Sys_Localization.Instance.GetLocalizedValue("welcome_message");
            Debug.Log(welcomeText);

            // Get localized text with parameters
            string greetingText = Sys_Localization.Instance.GetLocalizedValue("greeting", "Player1");
            Debug.Log(greetingText);

            // Check if key exists
            if (Sys_Localization.Instance.HasKey("start_button"))
            {
                Debug.Log("Start button text found");
            }
        }

        public void OnLanguageButtonClicked(string languageCode)
        {
            // Change language
            Sys_Localization.Instance.ChangeLanguage(languageCode);
        }
    }
}