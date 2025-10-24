
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Input
{
    public class SInput : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;



        readonly EcsPoolInject<R_MouseMapPositionCheck> mouseMapPositionCheckRPool = default;

        readonly EcsPoolInject<R_MouseMapClickCheck> mouseMapClickCheckRPool = default;


        readonly EcsCustomInject<InputData> inputData = default;

        public void Run(IEcsSystems systems)
        {
            //��������� ��������� ������� ���� �� ������� �� ��������� �������������� � ������
            MousePositionChangeRequests();
        }

        readonly EcsFilterInject<Inc<R_MousePositionChange>> mousePositionChangeRFilter = default;
        readonly EcsPoolInject<R_MousePositionChange> mousePositionChangeRPool = default;
        void MousePositionChangeRequests()
        {
            //��� ������� ������� ��������� ��������� �������
            foreach (int requestEntity in mousePositionChangeRFilter.Value)
            {
                //���� ������
                ref R_MousePositionChange requestComp = ref mousePositionChangeRPool.Value.Get(requestEntity);

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
            ref R_MousePositionChange requestComp)
        {
            //��������� ������ �� �������
            inputData.Value.isMouseOverMap = requestComp.isMouseOverMap;
            inputData.Value.lastHitProvincePE = requestComp.lastHitProvincePE;
        }
    }
}
