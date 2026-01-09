
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

        readonly EcsFilterInject<Inc<R_Map_Activation>> map_Activation_R_F = default;
        readonly EcsPoolInject<R_Map_Activation> map_Activation_R_P = default;
        void Maps_Activation()
        {
            //Для каждого запроса активации карты
            foreach(int requestEntity in map_Activation_R_F.Value)
            {
                //Берём запрос
                ref R_Map_Activation requestComp = ref map_Activation_R_P.Value.Get(requestEntity);

                //Если текущая активная карта была деактивирована
                if(Map_DeactivationCheck(ref requestComp))
                {
                    //Активируем запрошенную карту
                    Map_Activation(ref requestComp);
                }

                //Удаляем запрос
                map_Activation_R_P.Value.Del(requestEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_ProvinceRender>> pR_F = default;
        /// <summary>
        /// Возвращает False, если запрошенная карта уже активна, то есть её не требуется деактивировать
        /// </summary>
        /// <param name="requestComp"></param>
        /// <returns></returns>
        bool Map_DeactivationCheck(
            ref R_Map_Activation requestComp)
        {
            //Если активна не та карта, которую требуется активировать
            if(mapRender_Data.Value.ActiveMapPE.EqualsTo(requestComp.mapPE) == false)
            {
                //Для каждой провинции с компонентом PR
                foreach(int provinceEntity in pR_F.Value)
                {
                    //Берём компонент PR

                    //Удаляем компонент с провинции
                    pR_P.Value.Del(provinceEntity);
                }

                //Удаляем PE активной карты
                mapRender_Data.Value.ActiveMapPE = new();

                //Возвращаем, что карта деактивирована
                return true;
            }

            return false;
        }

        readonly EcsPoolInject<C_Map> map_P = default;
        void Map_Activation(
            ref R_Map_Activation requestComp)
        {
            //Берём запрошенную карту
            requestComp.mapPE.Unpack(world.Value, out int mapEntity);
            ref C_Map map = ref map_P.Value.Get(mapEntity);

            //Сохраняем PE карты как активной
            mapRender_Data.Value.ActiveMapPE = world.Value.PackEntity(mapEntity);

            //Для каждой провинции карты
            for (int a = 0; a < map.provinceEntities.Length; a++)
            {
                //Берём сущность провинции и назначаем ей компонент PR
                ref C_ProvinceRender pR = ref pR_P.Value.Add(map.provinceEntities[a]);

                //Заполняем данные PR
                pR = new(0);
            }

            //Запрашиваем инициализацию карты
            Map_RenderInitialization_Request();

            //Запрашиваем активацию стандартного режима карты
            MapMode_Default_Activation();
        }

        readonly EcsPoolInject<R_Map_RenderInitialization> map_RenderInitialization_R_P = default;
        void Map_RenderInitialization_Request()
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.Value.NewEntity();
            ref R_Map_RenderInitialization requestComp = ref map_RenderInitialization_R_P.Value.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        readonly EcsPoolInject<R_MapMode_Activation> mM_Activation_R_P = default;
        void MapMode_Default_Activation()
        {
            //Запрашиваем активацию стандартного режима карты
            MainMapMode_Data.MapMode_Activation_Request(
                world.Value,
                mM_Activation_R_P.Value,
                mainMapMode_Data.Value.DefaultMapModePE);
        }
    }
}
