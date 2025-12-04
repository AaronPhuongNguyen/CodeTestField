using UnityEngine;

namespace Game.System.Core
{
    /// <summary>
    /// Component to automatically localize UI Text elements
    /// Supports both Unity UI Text and TextMeshPro
    /// </summary>
    /// 
    [DefaultExecutionOrder(-10)]
    public class LocalizedText : Sys_Components
    {
        #region Inspector Fields
        [Tooltip("Localization key to display")]
        public string key;

        [Tooltip("Format parameters for dynamic text")]
        public string[] formatParams;
        #endregion

        #region Private Fields
        private UnityEngine.UI.Text textComponent;
        private TMPro.TextMeshProUGUI tmpComponent;
        #endregion

        #region Lifecycle
        protected override void Wake()
        {
            // Cache text components
            textComponent = GetComponent<UnityEngine.UI.Text>();
            tmpComponent = GetComponent<TMPro.TextMeshProUGUI>();
        }

        protected override void Init()
        {
            // Subscribe to language change event
            if (Sys_Localization.Instance != null)
            {
                Sys_Localization.Instance.OnLanguageChanged += UpdateText;
            }

            UpdateText();
        }

        private void OnDestroy()
        {
            // Unsubscribe from events
            if (Sys_Localization.Instance != null)
            {
                Sys_Localization.Instance.OnLanguageChanged -= UpdateText;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update text component with localized value
        /// </summary>
        public void UpdateText()
        {
            if (Sys_Localization.Instance == null || string.IsNullOrEmpty(key))
                return;

            string localizedValue;

            // Apply format parameters if provided
            if (formatParams != null && formatParams.Length > 0)
            {
                localizedValue = Sys_Localization.Instance.GetLocalizedValue(key, formatParams);
            }
            else
            {
                localizedValue = Sys_Localization.Instance.GetLocalizedValue(key);
            }

            // Set text to appropriate component
            if (textComponent != null)
            {
                textComponent.text = localizedValue;
            }

            if (tmpComponent != null)
            {
                tmpComponent.text = localizedValue;
            }
        }

        /// <summary>
        /// Change localization key and update text
        /// </summary>
        public void SetKey(string newKey)
        {
            key = newKey;
            UpdateText();
        }

        /// <summary>
        /// Set format parameters and update text
        /// </summary>
        public void SetFormatParams(params string[] parameters)
        {
            formatParams = parameters;
            UpdateText();
        }
        #endregion
    }
}