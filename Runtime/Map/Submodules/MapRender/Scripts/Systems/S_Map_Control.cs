
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class S_Map_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ProvinceRender> pR_P = default;


        readonly EcsCustomInject<MapRender_Data> mapRender_Data = default;
        readonly EcsCustomInject<MainMapMode_Data> mainMapMode_Data = default;

        public void Run(IEcsSystems systems)
        {
            //Активируем карту по запросу
            Maps_Activation();
        }

        readonly EcsFilterInject<Inc<C_Map, SR_Map_Activation>> map_Activation_R_F = default;
        void Maps_Activation()
        {
            //Для каждого запроса активации карты
            foreach(int mEntity in map_Activation_R_F.Value)
            {
                //Берём запрос
                ref SR_Map_Activation rComp = ref map_Activation_R_F.Pools.Inc2.Get(mEntity);

                //Если запрошенная карта не была активна
                if(Map_DeactivationCheck(mEntity))
                {
                    //Активируем запрошенную карту
                    Map_Activation(mEntity);
                }

                //Удаляем запрос
                map_Activation_R_F.Pools.Inc2.Del(mEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_ProvinceCore, C_ProvinceRender>> pR_F = default;
        /// <summary>
        /// Возвращает False, если запрошенная карта уже активна, то есть её не требуется деактивировать
        /// </summary>
        /// /// <param name="mEntity"></param>
        /// <returns></returns>
        bool Map_DeactivationCheck(
            int mEntity)
        {
            //Если активна не та карта, которую требуется активировать
            if(mapRender_Data.Value.ActiveMapEntity != mEntity)
            {
                //Для каждой провинции с компонентом PR
                foreach(int pEntity in pR_F.Value)
                {
                    //Удаляем компонент рендеринга с провинции
                    pR_F.Pools.Inc2.Del(pEntity);
                }

                //Удаляем сущность активной карты
                mapRender_Data.Value.ActiveMapEntity = -1;

                //Возвращаем, что карта деактивирована
                return true;
            }

            return false;
        }

        readonly EcsPoolInject<C_Map> map_P = default;
        void Map_Activation(
            int mEntity)
        {
            //Берём запрошенную карту
            ref C_Map map = ref map_P.Value.Get(mEntity);

            //Отмечаем её как активную
            mapRender_Data.Value.ActiveMapEntity = mEntity;

            //Для каждой провинции карты
            for (int a = 0; a < map.provinceEntities.Length; a++)
            {
                //Берём сущность провинции и назначаем ей компонент PR
                ref C_ProvinceRender pR = ref pR_P.Value.Add(map.provinceEntities[a]);

                //Заполняем данные PR
                pR = new(0);
            }

            //Запрашиваем инициализацию рендера карты
            Map_RenderInitialization_Request(mEntity);

            //Запрашиваем активацию стандартного режима карты
            MapMode_Default_Activation();
        }

        readonly EcsPoolInject<SR_Map_RenderInitialization> map_RenderInitialization_R_P = default;
        void Map_RenderInitialization_Request(
            int mEntity)
        {
            //Если у сущности ещё нет запроса инициализации
            if (map_RenderInitialization_R_P.Value.Has(mEntity) == false)
            {
                //Назначаем переданной сущности запрос инициализации рендеринга карты
                ref SR_Map_RenderInitialization rComp = ref map_RenderInitialization_R_P.Value.Add(mEntity);

                //Заполняем данные запроса
                rComp = new(0);
            }
        }

        readonly EcsPoolInject<R_MapMode_Activation> mM_Activation_R_P = default;
        void MapMode_Default_Activation()
        {
            //Запрашиваем активацию стандартного режима карты
            MainMapMode_Data.MapMode_Activation_R(
                world.Value,
                mM_Activation_R_P.Value,
                mainMapMode_Data.Value.DefaultMapModePE);
        }
    }
}
