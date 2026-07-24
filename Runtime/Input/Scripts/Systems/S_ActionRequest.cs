
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public enum ActionType : byte
    {
        CameraRight,
        CameraLeft,
        CameraUp,
        CameraDown,
        CameraZoomIn,
        CameraZoomOut
    }

    public class S_ActionRequest : IProtoRunSystem
    {
        [DI] A_Input input_A;

        [DI] Input_Data input_Data;

        public void Run()
        {
            //Проверяем клавиши, связанные с нампадом
            Keypad_ActionRequests();
        }

        void Keypad_ActionRequests()
        {
            if (input_Data.rightArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraRight);
            }

            if (input_Data.leftArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraLeft);
            }

            if (input_Data.upArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraUp);
            }

            if (input_Data.downArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraDown);
            }

            if (input_Data.keypadPlusPressed == true)
            {
                CheckActionType(ActionType.CameraZoomIn);
            }

            if (input_Data.keypadMinusPressed == true)
            {
                CheckActionType(ActionType.CameraZoomOut);
            }
        }

        void CheckActionType(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.CameraRight:
                    Camera_Moving_Request(
                        true, false, false,
                        1);
                    break;
                case ActionType.CameraLeft:
                    Camera_Moving_Request(
                        true, false, false,
                        -1);
                    break;
                case ActionType.CameraUp:
                    Camera_Moving_Request(
                        false, true, false,
                        1);
                    break;
                case ActionType.CameraDown:
                    Camera_Moving_Request(
                        false, true, false,
                        -1);
                    break;
                case ActionType.CameraZoomIn:
                    Camera_Moving_Request(
                        false, false, true,
                        1);
                    break;
                case ActionType.CameraZoomOut:
                    Camera_Moving_Request(
                        false, false, true,
                        -1);
                    break;
            }
        }

        void Camera_Moving_Request(
            bool isHorizontal, bool isVertical, bool isZoom,
            float value)
        {
            //Если камера не заблокирована
            if (input_Data.isCameraBlocked == false)
            {
                //Создаём новую сущность и назначаем ей запрос движения камеры
                ref R_Camera_Moving rComp = ref input_A.camera_Moving_R_P.NewEntity(out ProtoEntity rEntity);

                //Заполняем данные запроса
                rComp = new R_Camera_Moving(
                    isHorizontal, isVertical, isZoom,
                    value);
            }
        }
    }
}