
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    /// <summary>
    /// Система, создающая основной компонент карты по запросу
    /// Отрабатывает в Init и PreTick
    /// </summary>
    public class S_Map_Creation : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsPoolInject<C_Map> map_P = default;

        public void Init(IEcsSystems systems)
        {
            //Создаём карты
            Maps_Creation();
        }

        public void Run(IEcsSystems systems)
        {
            //Создаём карты
            Maps_Creation();
        }

        readonly EcsFilterInject<Inc<SR_Map_Creation>> map_Creation_SR_F = default;
        readonly EcsPoolInject<SR_Map_Creation> map_Creation_SR_P = default;
        void Maps_Creation()
        {
            //Для каждого запроса создания карты
            foreach(int mapRequestEntity in map_Creation_SR_F.Value)
            {
                //Берём запрос
                ref SR_Map_Creation requestComp = ref map_Creation_SR_P.Value.Get(mapRequestEntity);

                //Создаём карту
                Map_Creation(
                    ref requestComp,
                    mapRequestEntity);

                //ТЕСТ
                //Берём карту
                /*ref C_Map map = ref mapPool.Value.Get(mapRequestEntity);

                if (true)
                {
                    //Запрашиваем активацию карты
                    MapData.MapActivationRequest(
                        world.Value,
                        mapActivationRPool.Value,
                        world.Value.PackEntity(mapRequestEntity));
                }*/
                //ТЕСТ

                //Удаляем запрос
                map_Creation_SR_P.Value.Del(mapRequestEntity);
            }
        }

        void Map_Creation(
            ref SR_Map_Creation requestComp,
            int mapEntity)
        {
            //Назначаем переданной сущности компонент карты
            ref C_Map map = ref map_P.Value.Add(mapEntity);

            //Заполняем основные данные карты
            map = new(
                requestComp.mapName);
        }
    }
}
