
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Core
{
    public class SEventsClear : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<E_ObjectCreated>> objectCreatedEFilter = default;
        readonly EcsPoolInject<E_ObjectCreated> objectCreatedEPool = default;

        public void Init(IEcsSystems systems)
        {
            //Очищаем события создания объектов
            ObjectCreatedEventsClear();
        }

        public void Run(IEcsSystems systems)
        {
            //Очищаем события создания объектов
            ObjectCreatedEventsClear();
        }

        void ObjectCreatedEventsClear()
        {
            //Для каждого события создания объекта
            foreach (int eventEntity in objectCreatedEFilter.Value)
            {
                //Удаляем компонент события
                objectCreatedEPool.Value.Del(eventEntity);
            }
        }
    }
}
