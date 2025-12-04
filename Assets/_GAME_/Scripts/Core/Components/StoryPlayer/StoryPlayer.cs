using Game.System.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.System.Interactive
{
    public class Comp_StoryPlayer : Sys_Components
    {
        public List<Game_Dialogue> _inputDialogue;
        public List<string> _ouputDialogues;
    }
}