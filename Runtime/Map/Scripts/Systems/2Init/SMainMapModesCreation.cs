
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
            //Создаём режимы карты по запросам
            MapModesCreation();
        }

        readonly EcsFilterInject<Inc<SR_MapModeCreation>> mapModeCreationSRFilter = default;
        readonly EcsPoolInject<SR_MapModeCreation> mapModeCreationSRPool = default;
        void MapModesCreation()
        {
            //Для каждого запроса создания режима карты
            foreach(int mapModeEntity in mapModeCreationSRFilter.Value)
            {
                //Берём запрос
                ref SR_MapModeCreation requestComp = ref mapModeCreationSRPool.Value.Get(mapModeEntity);

                //Создаём режим карты
                MapModeCreation(
                    mapModeEntity,
                    ref requestComp);

                //ТЕСТ
                //Берём режим карты
                ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

                //Если данный режим карты указан как стандартный
                if(requestComp.defaultMapMode == true)
                {
                    //Сохраняем его как стандартный режим карты
                    mapModeData.Value.defaultMapModePE = world.Value.PackEntity(mapModeEntity);
                }
                //ТЕСТ

                //Удаляем запрос
                mapModeCreationSRPool.Value.Del(mapModeEntity);
            }
        }

        readonly EcsPoolInject<C_MapModeCore> mapModeCorePool = default;
        void MapModeCreation(
            int mapModeEntity,
            ref SR_MapModeCreation requestComp)
        {
            //Назначаем сущности режима карты компонент режима карты
            ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Add(mapModeEntity);

            //Заполняем основные данные режима
            mapMode = new(
                requestComp.name);
        }
    }
}
