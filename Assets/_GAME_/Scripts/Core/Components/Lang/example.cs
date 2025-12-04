using Game.System.Core;
using UnityEngine;

public class LocalizationExample : Sys_Components
{
    protected override void Init()
    {
        string welcomeText = Sys_Localization.Instance.GetLocalizedValue("welcome_message");
        Debug.Log(welcomeText);
        string greetingText = Sys_Localization.Instance.GetLocalizedValue("greeting", "Player1");
        Debug.Log(greetingText);
        if (Sys_Localization.Instance.HasKey("start_button"))
        {
            Debug.Log("Start button text found");
        }
    }

    public void OnLanguageButtonClicked(string languageCode)
    {
        Sys_Localization.Instance.ChangeLanguage(languageCode);
    }
}