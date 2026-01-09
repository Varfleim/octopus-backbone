
using Leopotam.EcsLite;

namespace GBB.Core
{
    public readonly struct E_Object_Created
    {
        public E_Object_Created(
            int objectTypeIndex,
            EcsPackedEntity objectPE)
        {
            this.objectTypeIndex = objectTypeIndex;

            this.objectPE = objectPE;
        }

        public readonly int objectTypeIndex;

        public readonly EcsPackedEntity objectPE;
    }
}
