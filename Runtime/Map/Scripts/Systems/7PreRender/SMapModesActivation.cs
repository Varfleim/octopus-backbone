
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModesActivation : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_MapModeCore> mapModeCorePool = default;
        readonly EcsPoolInject<CT_ActiveMapMode> activeMapModePool = default;


        readonly EcsPoolInject<R_MapProvincesUpdate> mapProvincesUpdateRPool = default;


        readonly EcsCustomInject<MapModeData> mapModeData = default;

        public void Run(IEcsSystems systems)
        {
            //Активируем режим карты по запросу
            MapModesActivation();
        }

        readonly EcsFilterInject<Inc<R_MapModeActivation>> mapModeActivationRFilter = default;
        readonly EcsPoolInject<R_MapModeActivation> mapModeActivationRPool = default;
        void MapModesActivation()
        {
            //Для каждого запроса активации режима карты
            foreach(int requestEntity in mapModeActivationRFilter.Value)
            {
                //Берём запрос
                ref R_MapModeActivation requestComp = ref mapModeActivationRPool.Value.Get(requestEntity);

                //Деактивируем активный режим карты
                bool isMapModeDeactivated = MapModeDeactivationCheck(ref requestComp);

                //Если режим карты деактивирован
                if(isMapModeDeactivated == true)
                {
                    //Активируем режим карты
                    MapModeActivation(ref requestComp);
                }

                //Удаляем запрос
                mapModeActivationRPool.Value.Del(requestEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_MapModeCore, CT_ActiveMapMode>> activeMapModeFilter = default;
        bool MapModeDeactivationCheck(
            ref R_MapModeActivation requestComp)
        {
            //Берём сущность запрошенного режима карты
            requestComp.mapModePE.Unpack(world.Value, out int requestedMapModeEntity);

            //Активный режим может быть только один, но нужно использовать цикл
            foreach (int activeMapModeEntity in activeMapModeFilter.Value)
            {
                //Берём активный режим карты
                ref C_MapModeCore activeMapMode = ref mapModeCorePool.Value.Get(activeMapModeEntity);

                //Если это не тот режим карты, который требуется активировать
                if (activeMapModeEntity != requestedMapModeEntity)
                {
                    //Удаляем компонент активного режима
                    activeMapModePool.Value.Del(activeMapModeEntity);
                    //Удаляем PE активного режима
                    mapModeData.Value.activeMapModePE = new();

                    //Возвращаем, что режим карты деактивирован
                    return true;
                }
                //Иначе
                else
                {
                    //Возвращаем, что режим карты не деактивирован
                    return false;
                }
            }

            //Возвращаем, что режим карты деактивирован
            return true;
        }

        void MapModeActivation(
            ref R_MapModeActivation requestComp)
        {
            //Берём запрошенный режим карты
            requestComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

            //Назначаем ему компонент активного режима
            activeMapModePool.Value.Add(mapModeEntity);
            //Сохраняем PE режима как активного
            mapModeData.Value.activeMapModePE = world.Value.PackEntity(mapModeEntity);

            //Удаляем все запросы обновления режимов карты
            MapModeUpdatesCancel();

            //Запрашиваем обновление режима карты
            MapModeData.MapModeUpdateRequest(
                mapModeUpdateSRPool.Value,
                mapModeEntity);

            //Запрашиваем обновление провинций карты
            MapData.MapProvincesUpdateRequest(
                world.Value,
                mapProvincesUpdateRPool.Value,
                true, false, false);
        }


        readonly EcsFilterInject<Inc<C_MapModeCore, SR_MapModeUpdate>> mapModeUpdateSRFilter = default;
        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;
        /// <summary>
        /// Удаление всех существующих запросов обновления режима карты
        /// </summary>
        void MapModeUpdatesCancel()
        {
            //Для каждого запроса обновления режима карты
            foreach(int mapModeEntity in mapModeUpdateSRFilter.Value)
            {
                //Удаляем запрос
                mapModeUpdateSRPool.Value.Del(mapModeEntity);
            }
        }
    }
}
