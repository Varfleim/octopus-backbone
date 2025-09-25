
using UnityEngine;

namespace GBB.Input
{
    [CreateAssetMenu]
    public class InputModule : GameModule
    {
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
            //������ ������ ��� ������ ����� � ��������� ��� �� ���������
            InputData inputData = startup.AddDataObject().AddComponent<InputData>();

            //������ ������
            startup.InjectData(inputData);
        }
    }
}
