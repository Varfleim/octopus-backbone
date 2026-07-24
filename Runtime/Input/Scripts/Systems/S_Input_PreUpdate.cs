
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public class S_Input_PreUpdate : IProtoRunSystem
    {
        [DI] Input_Data input_Data;

        public void Run()
        {
            //Обновляем состояние кнопок мыши
            MouseButtons_Update();
        }

        void MouseButtons_Update()
        {
            //Определяем состояние ЛКМ
            input_Data.leftMouseButtonClick = UnityEngine.Input.GetMouseButtonDown(0);
            input_Data.leftMouseButtonPressed = input_Data.leftMouseButtonClick || UnityEngine.Input.GetMouseButton(0);
            input_Data.leftMouseButtonRelease = UnityEngine.Input.GetMouseButtonUp(0);

            //Определяем состояние ПКМ
            input_Data.rightMouseButtonClick = UnityEngine.Input.GetMouseButtonDown(1);
            input_Data.rightMouseButtonPressed = input_Data.leftMouseButtonClick || UnityEngine.Input.GetMouseButton(1);
            input_Data.rightMouseButtonRelease = UnityEngine.Input.GetMouseButtonUp(1);
        }
    }
}
