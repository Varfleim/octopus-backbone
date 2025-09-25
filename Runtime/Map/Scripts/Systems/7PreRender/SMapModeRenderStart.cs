
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;

namespace GBB.Map
{
    public class SMapModeRenderStart : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<EcsGroupSystemState> ecsGroupSystemStatePool = default;

        public void Run(IEcsSystems systems)
        {
            //�������� ������� ������������ ������� �����
            MapModesRenderSystemsActivation();
        }

        readonly EcsFilterInject<Inc<CMapModeCore, SRMapModeUpdate>> mapModeUpdateSRFilter = default;
        readonly EcsPoolInject<CMapModeCore> mapModeCorePool = default;
        void MapModesRenderSystemsActivation()
        {
            //��� ������� ������ ����� � �������� ����������
            foreach (int mapModeEntity in mapModeUpdateSRFilter.Value)
            {
                //���� ����� �����
                ref CMapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

                //������ ����� �������� � ��������� �� ������ ������������ ������ ������
                int requestEntity = world.Value.NewEntity();
                ref EcsGroupSystemState requestComp = ref ecsGroupSystemStatePool.Value.Add(requestEntity);

                //��������� ������ �������
                requestComp.Name = mapMode.selfName;
                requestComp.State = true;
            }
        }
    }
}
