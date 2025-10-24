
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
            //������� ������� �������� ��������
            ObjectCreatedEventsClear();
        }

        public void Run(IEcsSystems systems)
        {
            //������� ������� �������� ��������
            ObjectCreatedEventsClear();
        }

        void ObjectCreatedEventsClear()
        {
            //��� ������� ������� �������� �������
            foreach (int eventEntity in objectCreatedEFilter.Value)
            {
                //������� ��������� �������
                objectCreatedEPool.Value.Del(eventEntity);
            }
        }
    }
}
