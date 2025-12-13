
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class SMapModesActivation : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;


        readonly EcsCustomInject<MainMapModeData> mainMapModeData = default;

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

                //Если текущий активный режим карты был деактивирован
                if(MapModeDeactivationCheck(ref requestComp))
                {
                    //Активируем режим карты
                    MapModeActivation(ref requestComp);
                }

                //Удаляем запрос
                mapModeActivationRPool.Value.Del(requestEntity);
            }
        }

        /// <summary>
        /// Возвращает False, если запрошенный режим карты уже активен, то есть его не требуется деактивировать
        /// </summary>
        /// <param name="requestComp"></param>
        /// <returns></returns>
        bool MapModeDeactivationCheck(
            ref R_MapModeActivation requestComp)
        {
            //Если активен не тот режим карты, который требуется активировать
            if(mainMapModeData.Value.ActiveMapModePE.EqualsTo(requestComp.mapModePE) == false)
            {
                //Удаляем PE активного режима
                mainMapModeData.Value.ActiveMapModePE = new();

                //Возвращаем, что режим карты деактивирован
                return true;
            }

            return false;
        }

        readonly EcsPoolInject<C_MapModeCore> mapModeCorePool = default;
        readonly EcsPoolInject<R_MapProvincesUpdate> mapProvincesUpdateRPool = default;
        void MapModeActivation(
            ref R_MapModeActivation requestComp)
        {
            //Берём запрошенный режим карты
            requestComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

            //Сохраняем PE режима как активного
            mainMapModeData.Value.ActiveMapModePE = world.Value.PackEntity(mapModeEntity);

            //Отменяем все запросы обновления режимов карты
            MapModeUpdatesCancel();

            //Запрашиваем обновление режима карты
            MainMapModeData.MapModeUpdateRequest(
                mapModeUpdateSRPool.Value,
                mapModeEntity);

            //Запрашиваем обновление провинций карты
            MapRenderData.MapProvincesUpdateRequest(
                world.Value,
                mapProvincesUpdateRPool.Value,
                true, false, false);
        }

        readonly EcsFilterInject<Inc<SR_MapModeUpdate>> mapModeUpdateSRFilter = default;
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
