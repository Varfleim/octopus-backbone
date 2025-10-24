
using Leopotam.EcsLite;

namespace GBB.Core
{
    public readonly struct E_ObjectCreated
    {
        public E_ObjectCreated(
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
