
using UnityEngine;

using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public class Input_Data : MonoBehaviour
    {
        #region Mouse
        public bool leftMouseButtonClick;
        public bool leftMouseButtonPressed;
        public bool leftMouseButtonRelease;

        public bool rightMouseButtonClick;
        public bool rightMouseButtonPressed;
        public bool rightMouseButtonRelease;

        public bool isMouseOverMap;
        public ProtoPackedEntity lastHitProvincePE;
        #endregion

        #region Keyboard
        public bool rightArrowKeyPressed;
        public bool leftArrowKeyPressed;
        public bool upArrowKeyPressed;
        public bool downArrowKeyPressed;

        public bool keypadPlusPressed;
        public bool keypadMinusPressed;
        #endregion

        #region Camera
        public bool isCameraBlocked = false;
        #endregion
    }
}
