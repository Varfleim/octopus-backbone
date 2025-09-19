
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Core
{
    public class CoreData : MonoBehaviour
    {
        public int seed;

        public static void ObjectCreatedEvent(
            EcsWorld world,
            EcsPool<EObjectCreated> eventPool,
            int objectTypeIndex,
            EcsPackedEntity objectPE)
        {
            //������ ����� �������� � ��������� �� ��������� �������
            int eventEntity = world.NewEntity();
            ref EObjectCreated eventComp = ref eventPool.Add(eventEntity);

            //��������� ������ �������
            eventComp = new(
                objectTypeIndex,
                objectPE);
        }
    }
}
