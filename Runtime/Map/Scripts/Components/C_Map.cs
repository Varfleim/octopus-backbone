
using Leopotam.EcsProto;

namespace GBB.Map
{
    public struct C_Map
    {
        public C_Map(
            ProtoEntity parentRenderEntity)
        {
            this.parentRenderEntity = parentRenderEntity;

            provinceEntities = new ProtoEntity[0];
        }

        public ProtoEntity parentRenderEntity;

        public ProtoEntity[] provinceEntities;

        public ProtoEntity Province_Get(
            int provinceIndex)
        {
            return provinceEntities[provinceIndex];
        }

        /// <summary>
        /// Ќельз€ использовать в многопоточных системах
        /// </summary>
        /// <returns></returns>
        public ProtoEntity Province_GetRandom()
        {
            return Province_Get(UnityEngine.Random.Range(0, provinceEntities.Length));
        }
    }
}
