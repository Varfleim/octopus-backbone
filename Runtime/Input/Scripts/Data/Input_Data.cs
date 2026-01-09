
using UnityEngine;

using Leopotam.EcsLite;

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
        public EcsPackedEntity lastHitProvincePE;

        public static void Mouse_MapPositionCheck_Request(
            EcsWorld world,
            EcsPool<R_Mouse_MapPositionCheck> r_P,
            EcsPackedEntity currentProvincePE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_Mouse_MapPositionCheck requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                currentProvincePE);
        }

        public static void Mouse_MapClickCheck_Request(
            EcsWorld world,
            EcsPool<R_Mouse_MapClickCheck> r_P,
            EcsPackedEntity currentProvincePE,
            bool leftMouseButtonClick, bool rightMouseButtonClick)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_Mouse_MapClickCheck requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                currentProvincePE,
                leftMouseButtonClick, rightMouseButtonClick);
        }
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
