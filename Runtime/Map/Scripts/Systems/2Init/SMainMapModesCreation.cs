
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

        readonly EcsFilterInject<Inc<SR_MapModeCreation>> mapModeCreationSRFilter = default;
        readonly EcsPoolInject<SR_MapModeCreation> mapModeCreationSRPool = default;
        void MapModesCreation()
        {
            //��� ������� ������� �������� ������ �����
            foreach(int mapModeEntity in mapModeCreationSRFilter.Value)
            {
                //���� ������
                ref SR_MapModeCreation requestComp = ref mapModeCreationSRPool.Value.Get(mapModeEntity);

                //������ ����� �����
                MapModeCreation(
                    mapModeEntity,
                    ref requestComp);

                //����
                //���� ����� �����
                ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

                //���� ������ ����� ����� ������ ��� �����������
                if(requestComp.defaultMapMode == true)
                {
                    //��������� ��� ��� ����������� ����� �����
                    mapModeData.Value.defaultMapModePE = world.Value.PackEntity(mapModeEntity);
                }
                //����

                //������� ������
                mapModeCreationSRPool.Value.Del(mapModeEntity);
            }
        }

        readonly EcsPoolInject<C_MapModeCore> mapModeCorePool = default;
        void MapModeCreation(
            int mapModeEntity,
            ref SR_MapModeCreation requestComp)
        {
            //��������� �������� ������ ����� ��������� ������ �����
            ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Add(mapModeEntity);

            //��������� �������� ������ ������
            mapMode = new(
                world.Value.PackEntity(mapModeEntity), requestComp.name);
        }
    }
}
