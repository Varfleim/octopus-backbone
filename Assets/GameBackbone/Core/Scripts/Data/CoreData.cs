
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
            //Создаём новую сущность и назначаем ей компонент события
            int eventEntity = world.NewEntity();
            ref EObjectCreated eventComp = ref eventPool.Add(eventEntity);

            //Заполняем данные события
            eventComp = new(
                objectTypeIndex,
                objectPE);
        }
    }
}
