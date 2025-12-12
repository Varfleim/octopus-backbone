
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class SMapControl : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ProvinceRender> pRPool = default;


        readonly EcsCustomInject<MapRenderData> mapRenderData = default;
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

                //Если текущая активная карта была деактивирована
                if(MapDeactivationCheck(ref requestComp))
                {
                    //Активируем запрошенную карту
                    MapActivation(ref requestComp);
                }

                //Удаляем запрос
                mapActivationRPool.Value.Del(requestEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_ProvinceRender>> pRFilter = default;
        /// <summary>
        /// Возвращает False, если запрошенная карта уже активна, то есть её не требуется деактивировать
        /// </summary>
        /// <param name="requestComp"></param>
        /// <returns></returns>
        bool MapDeactivationCheck(
            ref R_MapActivation requestComp)
        {
            //Если активна не та карта, которую требуется активировать
            if(mapRenderData.Value.ActiveMapPE.EqualsTo(requestComp.mapPE) == false)
            {
                //Для каждой провинции с компонентом PR
                foreach(int provinceEntity in pRFilter.Value)
                {
                    //Берём компонент PR

                    //Удаляем компонент с провинции
                    pRPool.Value.Del(provinceEntity);
                }

                //Удаляем PE активной карты
                mapRenderData.Value.ActiveMapPE = new();

                //Возвращаем, что карта деактивирована
                return true;
            }

            return false;
        }

        readonly EcsPoolInject<C_Map> mapPool = default;
        readonly EcsPoolInject<R_MapRenderInitialization> mapRenderInitializationRPool = default;
        void MapActivation(
            ref R_MapActivation requestComp)
        {
            //Берём запрошенную карту
            requestComp.mapPE.Unpack(world.Value, out int mapEntity);
            ref C_Map map = ref mapPool.Value.Get(mapEntity);

            //Сохраняем PE карты как активной
            mapRenderData.Value.ActiveMapPE = world.Value.PackEntity(mapEntity);

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
            MapRenderData.MapRenderInitializationRequest(
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
