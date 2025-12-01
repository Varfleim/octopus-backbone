
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapControl : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_Map> mapPool = default;
        readonly EcsPoolInject<CT_ActiveMap> activeMapPool = default;

        readonly EcsFilterInject<Inc<C_ProvinceCore, C_ProvinceRender>> pRFilter = default;
        readonly EcsPoolInject<C_ProvinceRender> pRPool = default;


        readonly EcsPoolInject<R_MapRenderInitialization> mapRenderInitializationRPool = default;


        readonly EcsCustomInject<MapData> mapData = default;
        readonly EcsCustomInject<MapModeData> mapModeData = default;

        public void Run(IEcsSystems systems)
        {
            //Активируем карту по запросу
            MapsActivation();
        }

        readonly EcsFilterInject<Inc<R_MapActivation>> mapActivationRFilter = default;
        readonly EcsPoolInject<R_MapActivation> mapActivationRPool = default;
        void MapsActivation()
        {
            //Для каждого запроса активации карты
            foreach(int requestEntity in mapActivationRFilter.Value)
            {
                //Берём запрос
                ref R_MapActivation requestComp = ref mapActivationRPool.Value.Get(requestEntity);

                //Деактивируем активную карту
                bool isMapDeactivated = MapDeactivation(ref requestComp);

                //Если карта была деактивирована
                if(isMapDeactivated == true)
                {
                    //Активируем запрошенную карту
                    MapActivation(ref requestComp);
                }

                //Удаляем запрос
                mapActivationRPool.Value.Del(requestEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_Map, CT_ActiveMap>> activeMapFilter = default;
        /// <summary>
        /// Возвращает False, если запрошенная карта уже активна, то есть её не требуется деактивировать
        /// </summary>
        /// <param name="requestComp"></param>
        /// <returns></returns>
        bool MapDeactivation(
            ref R_MapActivation requestComp)
        {
            //Берём сущность запрошенной карты
            requestComp.mapPE.Unpack(world.Value, out int requestedMapEntity);

            //Активная карта может быть только одна, но нужно использовать цикл
            foreach (int activeMapEntity in activeMapFilter.Value)
            {
                //Берём активную карту
                ref C_Map activeMap = ref mapPool.Value.Get(activeMapEntity);

                //Если это не та карта, которую требуется активировать
                if(activeMapEntity != requestedMapEntity)
                {
                    //Для каждой провинции с компонентом PR
                    foreach (int provinceEntity in pRFilter.Value)
                    {
                        //Берём компонент PR

                        //Удаляем компонент с провинции
                        pRPool.Value.Del(provinceEntity);
                    }

                    //Удаляем компонент активной карты
                    activeMapPool.Value.Del(activeMapEntity);
                    //Удаляем PE активной карты
                    mapData.Value.activeMapPE = new();

                    //Возвращаем, что карта деактивирована
                    return true;
                }
                //Иначе
                else
                {
                    //Возвращаем, что карта не деактивирована
                    return false;
                }
            }

            //Возвращаем, что карта деактивирована
            return true;
        }

        void MapActivation(
            ref R_MapActivation requestComp)
        {
            //Берём запрошенную карту
            requestComp.mapPE.Unpack(world.Value, out int mapEntity);
            ref C_Map map = ref mapPool.Value.Get(mapEntity);

            //Назначаем ей компонент активной карты
            activeMapPool.Value.Add(mapEntity);
            //Сохраняем PE карты как PE активной
            mapData.Value.activeMapPE = world.Value.PackEntity(mapEntity);

            //Для каждой провинции карты
            for (int a = 0; a < map.provincePEs.Length; a++)
            {
                //Берём сущность провинции и назначаем ей компонент PR
                map.provincePEs[a].Unpack(world.Value, out int provinceEntity);
                ref C_ProvinceRender pR = ref pRPool.Value.Add(provinceEntity);

                //Заполняем данные PR
                pR = new(0);
            }

            //Запрашиваем инициализацию карты
            MapData.MapRenderInitializationRequest(
                world.Value,
                mapRenderInitializationRPool.Value);

            //Запрашиваем активацию стандартного режима карты
            MapModeDefaultActivation();
        }

        readonly EcsPoolInject<R_MapModeActivation> mapModeActivationRPool = default;
        void MapModeDefaultActivation()
        {
            //Запрашиваем активацию стандартного режима карты
            MapModeData.MapModeActivationRequest(
                world.Value,
                mapModeActivationRPool.Value,
                mapModeData.Value.defaultMapModePE);
        }
    }
}
