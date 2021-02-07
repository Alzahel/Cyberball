using UnityEngine;
using UnityEngine.UI;

namespace UI.Killfeed
{
    public class KillfeedItem : MonoBehaviour
    {
        [SerializeField]
        Text text = null;

        public void setup(string _deadPlayer, string _source)
        {
            text.text = "<color=green><b>"+ _source + "</b> </color> killed <color=Red><i>"+ _deadPlayer + "</i></color>";
        }
    }
}
