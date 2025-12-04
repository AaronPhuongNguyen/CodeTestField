using UnityEngine;
namespace Game.System.Core
{
    [CreateAssetMenu(menuName ="Create/Dialogue")]
    public class Game_Dialogue : ScriptableObject
    {
        [SerializeField,Tooltip("To confirm only one")]
        protected string _dialogueID="1234";

        [SerializeField] protected string _title="No way home";
        [SerializeField] protected string _speakerName="Micheal Jackson";

        [SerializeField,Tooltip("Must be using Language Index if working with Lang system")]
        protected string _contents="Ay yo chill~";

        public string GetID() => _dialogueID == null? "No ID":_dialogueID ;
        public string GetTitle() => _title == null? "...": _title;
        public string GetSpeakerName() => _speakerName==null?"?":_speakerName;
        public string GetContents() => _contents==null?" ":_speakerName;        
    }
}