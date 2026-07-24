
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public class S_Keyboard_Input : IProtoRunSystem
    {
        [DI] Input_Data input_Data;

        public void Run()
        {
            //Обновляем состояние клавиш нампада
            Keypad_Update();
        }

        void Keypad_Update()
        {
            //Определяем состояние клавиш стрелок
            input_Data.rightArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow);
            input_Data.leftArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow);
            input_Data.upArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.UpArrow);
            input_Data.downArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.DownArrow);

            //Определяем состояние клавиш математических символов
            input_Data.keypadPlusPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadPlus);
            input_Data.keypadMinusPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadMinus);
        }
    }
}