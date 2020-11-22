using UnityEngine;

namespace Cyberball.Utils
{
    public class SetLayerRecursively
    {
        public static void setLayer(GameObject _obj, int _newLayer)
        {
            if (_obj == null) return;

            _obj.layer = _newLayer;

            foreach (Transform _child in _obj.transform)
            {
                if (_child == null) continue;

                setLayer(_child.gameObject, _newLayer);
            }
        }
    }
}