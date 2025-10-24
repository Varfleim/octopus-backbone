
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;

namespace GBB.Map
{
    public class SMapModeRenderEnd : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<EcsGroupSystemState> ecsGroupSystemStatePool = default;

        public void Run(IEcsSystems systems)
        {
            //��������� ������� ������������ ������� �����
            MapModesRenderSystemsDeactivation();
        }

        readonly EcsFilterInject<Inc<C_MapModeCore, SR_MapModeUpdate>> mapModeUpdateSRFilter = default;
        readonly EcsPoolInject<C_MapModeCore> mapModeCorePool = default;
        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;
        void MapModesRenderSystemsDeactivation()
        {
            //��� ������� ������ ����� � �������� ����������
            foreach (int mapModeEntity in mapModeUpdateSRFilter.Value)
            {
                //���� ����� �����
                ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

                //������ ����� �������� � ��������� �� ������ ������������ ������ ������
                int requestEntity = world.Value.NewEntity();
                ref EcsGroupSystemState requestComp = ref ecsGroupSystemStatePool.Value.Add(requestEntity);

                //��������� ������ �������
                requestComp.Name = mapMode.selfName;
                requestComp.State = false;

                //������� ������ ���������� ������ �����
                mapModeUpdateSRPool.Value.Del(mapModeEntity);
            }
        }
    }
}
