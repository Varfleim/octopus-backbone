
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Input
{
    public class SInput : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsFilterInject<Inc<RMousePositionChange>> mousePositionChangeRFilter = default;
        readonly EcsPoolInject<RMousePositionChange> mousePositionChangeRPool = default;

        readonly EcsPoolInject<RMouseMapPositionCheck> mouseMapPositionCheckRPool = default;

        readonly EcsPoolInject<RMouseMapClickCheck> mouseMapClickCheckRPool = default;


        readonly EcsCustomInject<InputData> inputData = default;

        public void Run(IEcsSystems systems)
        {
            //��������� ��������� ������� ���� �� ������� �� ��������� �������������� � ������
            MousePositionChangeRequests();
        }

        void MousePositionChangeRequests()
        {
            //��� ������� ������� ��������� ��������� �������
            foreach (int requestEntity in mousePositionChangeRFilter.Value)
            {
                //���� ������
                ref RMousePositionChange requestComp = ref mousePositionChangeRPool.Value.Get(requestEntity);

                //��������� ��������� ������� ����
                MousePositionChangeRequest(ref requestComp);

                //���� ������ ��������� ��� ������
                if (inputData.Value.isMouseOverMap == true)
                {
                    //����������� �������� ��������� ������� �� �����
                    InputData.MouseMapPositionCheckRequest(
                        world.Value,
                        mouseMapPositionCheckRPool.Value,
                        inputData.Value.lastHitProvincePE);

                    //���� ���� ����� ��� ������ ������� ����
                    if (inputData.Value.leftMouseButtonClick == true
                        || inputData.Value.rightMouseButtonClick == true)
                    {
                        //����������� �������� ����� �� �����
                        InputData.MouseMapClickCheckRequest(
                            world.Value,
                            mouseMapClickCheckRPool.Value,
                            inputData.Value.lastHitProvincePE,
                            inputData.Value.leftMouseButtonClick, inputData.Value.rightMouseButtonClick);
                    }
                }

                //������� ������
                mousePositionChangeRPool.Value.Del(requestEntity);
            }
        }

        void MousePositionChangeRequest(
            ref RMousePositionChange requestComp)
        {
            //��������� ������ �� �������
            inputData.Value.isMouseOverMap = requestComp.isMouseOverMap;
            inputData.Value.lastHitProvincePE = requestComp.lastHitProvincePE;
        }
    }
}
