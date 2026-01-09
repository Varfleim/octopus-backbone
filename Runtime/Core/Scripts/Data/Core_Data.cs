
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Core
{
    public class Core_Data : MonoBehaviour
    {
        public int Seed
        {
            get
            {
                return seed;
            }
        }
        [SerializeField]
        private int seed;

        public static void Object_Created_Event(
            EcsWorld world,
            EcsPool<E_Object_Created> e_P,
            int objectTypeIndex,
            EcsPackedEntity objectPE)
        {
            //Создаём новую сущность и назначаем ей компонент события
            int eventEntity = world.NewEntity();
            ref E_Object_Created eventComp = ref e_P.Add(eventEntity);

            //Заполняем данные события
            eventComp = new(
                objectTypeIndex,
                objectPE);
        }
    }
}
