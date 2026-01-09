
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Input
{
    public class S_Keyboard_Input : IEcsRunSystem
    {
        readonly EcsCustomInject<Input_Data> input_Data = default;

        public void Run(IEcsSystems systems)
        {
            //Обновляем состояние клавиш нампада
            Keypad_Update();
        }

        void Keypad_Update()
        {
            //Определяем состояние клавиш стрелок
            input_Data.Value.rightArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow);
            input_Data.Value.leftArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow);
            input_Data.Value.upArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.UpArrow);
            input_Data.Value.downArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.DownArrow);

            //Определяем состояние клавиш математических символов
            input_Data.Value.keypadPlusPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadPlus);
            input_Data.Value.keypadMinusPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadMinus);
        }
    }
}
