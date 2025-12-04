using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;

namespace Game.System.Core
{
    /// <summary>
    /// Represents a single localization entry with key-value pair
    /// </summary>
    [Serializable]
    public class LocalizationEntry
    {
        public string key;
        public string value;
    }

    /// <summary>
    /// Main localization manager that handles loading and managing language files from XML
    /// Supports multiple languages with dynamic switching
    /// </summary>
    [DefaultExecutionOrder(-90)]
    public class Sys_Localization : Sys_Components
    {
        #region Singleton
        public static Sys_Localization Instance { get; private set; }
        #endregion

        #region Events
        /// <summary>
        /// Invoked when language is changed
        /// </summary>
        public event Action OnLanguageChanged;
        #endregion

        #region Private Fields
        private Dictionary<string, string> localizedText;
        private string currentLanguage = "vi";
        private bool isLoaded = false;
        #endregion

        #region Properties
        /// <summary>
        /// Get current active language code
        /// </summary>
        public string CurrentLanguage => currentLanguage;

        /// <summary>
        /// Check if localization data is loaded
        /// </summary>
        public bool IsLoaded => isLoaded;
        #endregion

        #region Lifecycle
        protected override void Wake()
        {
            // Setup singleton
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            localizedText = new Dictionary<string, string>();
        }

        protected override void Init()
        {
            // Load default language on initialization
            LoadLanguage(currentLanguage);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load language from XML file
        /// XML file should be located in StreamingAssets/Localization/{languageCode}.xml
        /// </summary>
        /// <param name="languageCode">Language code (e.g., "en", "vi", "ja")</param>
        public void LoadLanguage(string languageCode)
        {
            currentLanguage = languageCode;
            localizedText.Clear();

            string filePath = Path.Combine(Application.streamingAssetsPath, "Localization", languageCode + ".xml");

            if (!File.Exists(filePath))
            {
                Debug.LogError($"[Localization] Language file not found: {filePath}");
                isLoaded = false;
                return;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                // Parse all entry nodes
                XmlNodeList entries = xmlDoc.SelectNodes("//localization/entry");

                foreach (XmlNode entryNode in entries)
                {
                    string key = entryNode.Attributes["key"]?.Value;
                    string value = entryNode.InnerText;

                    if (!string.IsNullOrEmpty(key))
                    {
                        localizedText[key] = value;
                    }
                }

                isLoaded = true;
                Debug.Log($"Localization loaded {localizedText.Count} entries for language: {languageCode}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Localization error loading language file: {e.Message}");
                isLoaded = false;
            }
        }

        /// <summary>
        /// Get localized text by key
        /// Returns the key itself if not found
        /// </summary>
        /// <param name="key">Localization key</param>
        /// <returns>Localized string value</returns>
        public string GetLocalizedValue(string key)
        {
            if (!isLoaded)
            {
                Debug.LogWarning("[Localization] Language not loaded yet!");
                return key;
            }

            if (localizedText.ContainsKey(key))
            {
                return localizedText[key];
            }
            else
            {
                Debug.LogWarning($"[Localization] Key not found: {key}");
                return key;
            }
        }

        /// <summary>
        /// Get localized text with format parameters
        /// Example: GetLocalizedValue("welcome", "Player1") for "Welcome {0}!"
        /// </summary>
        public string GetLocalizedValue(string key, params object[] args)
        {
            string value = GetLocalizedValue(key);

            try
            {
                return string.Format(value, args);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[Localization] Format error for key '{key}': {e.Message}");
                return value;
            }
        }

        /// <summary>
        /// Change current language and reload data
        /// Triggers OnLanguageChanged event
        /// </summary>
        /// <param name="languageCode">New language code</param>
        public void ChangeLanguage(string languageCode)
        {
            if (currentLanguage == languageCode && isLoaded)
            {
                Debug.Log($"[Localization] Language '{languageCode}' already loaded");
                return;
            }

            LoadLanguage(languageCode);
            OnLanguageChanged?.Invoke();
        }

        /// <summary>
        /// Check if a key exists in current language
        /// </summary>
        public bool HasKey(string key)
        {
            return isLoaded && localizedText.ContainsKey(key);
        }
        #endregion
    }

    /// <summary>
    /// Component to automatically localize UI Text elements
    /// Supports both Unity UI Text and TextMeshPro
    /// </summary>

    /// <summary>
    /// Example usage of localization system
    /// </summary>
    
}