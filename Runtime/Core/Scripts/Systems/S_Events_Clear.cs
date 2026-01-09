
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Core
{
    public class S_Events_Clear : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<E_Object_Created>> object_Created_E_F = default;
        readonly EcsPoolInject<E_Object_Created> object_Created_E_P = default;

        public void Init(IEcsSystems systems)
        {
            //Очищаем события создания объектов
            Object_Created_Events_Clear();
        }

        public void Run(IEcsSystems systems)
        {
            //Очищаем события создания объектов
            Object_Created_Events_Clear();
        }

        void Object_Created_Events_Clear()
        {
            //Для каждого события создания объекта
            foreach (int eventEntity in object_Created_E_F.Value)
            {
                //Удаляем компонент события
                object_Created_E_P.Value.Del(eventEntity);
            }
        }
    }
}
