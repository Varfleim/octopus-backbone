
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Core
{
    public class CoreData : MonoBehaviour
    {
        public int seed;

        public static void ObjectCreatedEvent(
            EcsWorld world,
            EcsPool<E_ObjectCreated> eventPool,
            int objectTypeIndex,
            EcsPackedEntity objectPE)
        {
            //������ ����� �������� � ��������� �� ��������� �������
            int eventEntity = world.NewEntity();
            ref E_ObjectCreated eventComp = ref eventPool.Add(eventEntity);

            //��������� ������ �������
            eventComp = new(
                objectTypeIndex,
                objectPE);
        }
    }
}
