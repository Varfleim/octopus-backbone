
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
            foreach(int mEntity in map_Creation_SR_F.Value)
            {
                //Берём запрос
                ref SR_Map_Creation rComp = ref map_Creation_SR_P.Value.Get(mEntity);

                //Создаём карту
                Map_Creation(
                    mEntity,
                    ref rComp);

                //Удаляем запрос
                map_Creation_SR_P.Value.Del(mEntity);
            }
        }

        void Map_Creation(
            int mEntity,
            ref SR_Map_Creation rComp)
        {
            //Назначаем переданной сущности компонент карты
            ref C_Map m = ref map_P.Value.Add(mEntity);

            //Заполняем основные данные карты
            m = new(0);
        }
    }
}
