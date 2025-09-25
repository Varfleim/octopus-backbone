
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModeUpdate : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            //����������� ���������� ��������� ������ �����
            MapModeActiveUpdate();
        }

        readonly EcsFilterInject<Inc<CMapModeCore, CActiveMapMode>> activeMapModeFilter = default;
        readonly EcsPoolInject<SRMapModeUpdate> mapModeUpdateSRPool = default;
        void MapModeActiveUpdate()
        {
            //��� ������� ��������� ������ �����
            foreach(int activeMapModeEntity in activeMapModeFilter.Value)
            {
                //����������� ���������� ������ �����
                MapModeData.MapModeUpdateRequest(
                    mapModeUpdateSRPool.Value,
                    activeMapModeEntity);
            }
        }
    }
}
