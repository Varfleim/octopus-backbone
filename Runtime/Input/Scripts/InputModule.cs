
using UnityEngine;

namespace GBB.Input
{
    internal class InputModule : GameSubmodule
    {
        [SerializeField]
        private InputData inputData;

        public override void AddSystems(GameStartup startup)
        {
            //��������� ������� �������������

            //��������� ���������� �������
            #region PreFrame
            //��������� ����������, ��������� ��� ������ ����� - �� ��������� �� ������ �������
            startup.AddPreFrameSystem(new SInputPreUpdate());
            //������������ ������� ������ �� ����������
            startup.AddPreFrameSystem(new SKeyboardInput());
            #endregion
            #region Frame
            //��������� ����������, ��������� ��� ������ ����� - ��������� �� ������ �������
            startup.AddFrameSystem(new SInput());
            //����������� �������� �� �������� ������
            startup.AddFrameSystem(new SActionRequest());
            #endregion

            //��������� ������� ����������

            //��������� ��������� �������

        }

        public override void InjectData(GameStartup startup)
        {
            //������ ������
            startup.InjectData(inputData);
        }
    }
}
