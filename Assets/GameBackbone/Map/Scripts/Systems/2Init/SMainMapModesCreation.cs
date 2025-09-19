
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMainMapModesCreation : IEcsInitSystem
    {
        readonly EcsWorldInject world = default;

        readonly EcsCustomInject<MapModeData> mapModeData = default;

        public void Init(IEcsSystems systems)
        {
            //������ ������ ����� �� ��������
            MapModesCreation();
        }

        readonly EcsFilterInject<Inc<SRMapModeCreation>> mapModeCreationSRFilter = default;
        readonly EcsPoolInject<SRMapModeCreation> mapModeCreationSRPool = default;
        void MapModesCreation()
        {
            //��� ������� ������� �������� ������ �����
            foreach(int mapModeEntity in mapModeCreationSRFilter.Value)
            {
                //���� ������
                ref SRMapModeCreation requestComp = ref mapModeCreationSRPool.Value.Get(mapModeEntity);

                //������ ����� ����� �� �������
                MapModeCreation(
                    mapModeEntity,
                    ref requestComp);

                //���� ����� �����
                ref CMapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

                //���� ������ ����� ����� ������ ��� �����������
                if(requestComp.defaultMapMode == true)
                {
                    //��������� ��� ��� ����������� ����� �����
                    mapModeData.Value.defaultMapModePE = mapMode.selfPE;
                }

                //������� ������
                mapModeCreationSRPool.Value.Del(mapModeEntity);
            }
        }

        readonly EcsPoolInject<CMapModeCore> mapModeCorePool = default;
        void MapModeCreation(
            int mapModeEntity,
            ref SRMapModeCreation requestComp)
        {
            //��������� �������� ������ ����� ��������� ������ �����
            ref CMapModeCore mapMode = ref mapModeCorePool.Value.Add(mapModeEntity);

            //��������� �������� ������ ������
            mapMode = new(
                world.Value.PackEntity(mapModeEntity), requestComp.name);
        }
    }
}
