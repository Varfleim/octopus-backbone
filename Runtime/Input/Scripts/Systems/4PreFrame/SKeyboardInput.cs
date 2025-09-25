
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Input
{
    public class SKeyboardInput : IEcsRunSystem
    {
        readonly EcsCustomInject<InputData> inputData = default;

        public void Run(IEcsSystems systems)
        {
            //��������� ��������� ������ �������
            UpdateKeypad();
        }

        void UpdateKeypad()
        {
            //���������� ��������� ������ �������
            inputData.Value.rightArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow);
            inputData.Value.leftArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow);
            inputData.Value.upArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.UpArrow);
            inputData.Value.downArrowKeyPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.DownArrow);

            //���������� ��������� ������ �������������� ��������
            inputData.Value.keypadPlusPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadPlus);
            inputData.Value.keypadMinusPressed = UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadMinus);
        }
    }
}
