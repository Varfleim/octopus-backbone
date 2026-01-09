
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Input
{
    public class S_Input_PreUpdate : IEcsRunSystem
    {
        readonly EcsCustomInject<Input_Data> input_Data = default;

        public void Run(IEcsSystems systems)
        {
            //Обновляем состояние кнопок мыши
            MouseButtons_Update();
        }

        void MouseButtons_Update()
        {
            //Определяем состояние ЛКМ
            input_Data.Value.leftMouseButtonClick = UnityEngine.Input.GetMouseButtonDown(0);
            input_Data.Value.leftMouseButtonPressed = input_Data.Value.leftMouseButtonClick || UnityEngine.Input.GetMouseButton(0);
            input_Data.Value.leftMouseButtonRelease = UnityEngine.Input.GetMouseButtonUp(0);

            //Определяем состояние ПКМ
            input_Data.Value.rightMouseButtonClick = UnityEngine.Input.GetMouseButtonDown(1);
            input_Data.Value.rightMouseButtonPressed = input_Data.Value.leftMouseButtonClick || UnityEngine.Input.GetMouseButton(1);
            input_Data.Value.rightMouseButtonRelease = UnityEngine.Input.GetMouseButtonUp(1);
        }
    }
}
