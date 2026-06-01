
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

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

    public class S_ActionRequest : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;

        readonly EcsCustomInject<Input_Data> input_Data = default;

        public void Run(IEcsSystems systems)
        {
            //Проверяем клавиши, связанные с нампадом
            Keypad_ActionRequests();
        }

        void Keypad_ActionRequests()
        {
            if (input_Data.Value.rightArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraRight);
            }

            if (input_Data.Value.leftArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraLeft);
            }

            if (input_Data.Value.upArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraUp);
            }

            if (input_Data.Value.downArrowKeyPressed == true)
            {
                CheckActionType(ActionType.CameraDown);
            }

            if (input_Data.Value.keypadPlusPressed == true)
            {
                CheckActionType(ActionType.CameraZoomIn);
            }

            if (input_Data.Value.keypadMinusPressed == true)
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

        readonly EcsPoolInject<R_Camera_Moving> camera_Moving_R_P = default;
        void Camera_Moving_Request(
            bool isHorizontal, bool isVertical, bool isZoom,
            float value)
        {
            //Если камера не заблокирована
            if (input_Data.Value.isCameraBlocked == false)
            {
                //Создаём новую сущность и назначаем ей запрос движения камеры
                int rEntity = world.Value.NewEntity();
                ref R_Camera_Moving rComp = ref camera_Moving_R_P.Value.Add(rEntity);

                //Заполняем данные запроса
                rComp = new R_Camera_Moving(
                    isHorizontal, isVertical, isZoom,
                    value);
            }
        }
    }
}