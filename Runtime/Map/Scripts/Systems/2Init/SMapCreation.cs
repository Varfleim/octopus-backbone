
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    /// <summary>
    /// Система, создающая основной компонент карты по запросу
    /// Отрабатывает в Init и PreTick
    /// </summary>
    public class SMapCreation : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_Map> mapPool = default;

        readonly EcsPoolInject<R_MapActivation> mapActivationRPool = default;

        public void Init(IEcsSystems systems)
        {
            //Создаём карты
            MapsCreation();
        }

        public void Run(IEcsSystems systems)
        {
            //Создаём карты
            MapsCreation();
        }

        readonly EcsFilterInject<Inc<SR_MapCreation>> mapCreationSRFilter = default;
        readonly EcsPoolInject<SR_MapCreation> mapCreationSRPool = default;
        void MapsCreation()
        {
            //Для каждого запроса создания карты
            foreach(int mapRequestEntity in mapCreationSRFilter.Value)
            {
                //Берём запрос
                ref SR_MapCreation requestComp = ref mapCreationSRPool.Value.Get(mapRequestEntity);

                //Создаём карту
                MapCreation(
                    ref requestComp,
                    mapRequestEntity);

                //ТЕСТ
                //Берём карту
                ref C_Map map = ref mapPool.Value.Get(mapRequestEntity);

                if (true)
                {
                    //Запрашиваем активацию карты
                    MapData.MapActivationRequest(
                        world.Value,
                        mapActivationRPool.Value,
                        world.Value.PackEntity(mapRequestEntity));
                }
                //ТЕСТ

                //Удаляем запрос
                mapCreationSRPool.Value.Del(mapRequestEntity);
            }
        }

        void MapCreation(
            ref SR_MapCreation requestComp,
            int mapEntity)
        {
            //Назначаем переданной сущности компонент карты
            ref C_Map map = ref mapPool.Value.Add(mapEntity);

            //Заполняем основные данные карты
            map = new(
                requestComp.mapName);
        }
    }
}
