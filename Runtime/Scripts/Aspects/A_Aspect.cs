
using System.Collections.Generic;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB
{
    public abstract class A_Aspect : ProtoAspectInject
    {
        public List<ProtoAspectInject> childrenAspects = new();

        public override void Init(ProtoWorld world)
        {
            //Базовая инициализация
            base.Init(world);

            //Заносим каждый дочерний аспект в мир
            for (int a = 0; a < childrenAspects.Count; a++)
            {
                childrenAspects[a].Init(world);
            }
        }

        public override void PostInit()
        {
            //Базовая постинициализация
            base.PostInit();

            //Постинициализируем каждый дочерний аспект
            for (int a = 0; a < childrenAspects.Count; a++)
            {
                childrenAspects[a].PostInit();
            }
        }
    }
}
