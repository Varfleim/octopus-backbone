
using Leopotam.EcsProto;

namespace GBB.Map
{
    public readonly struct SR_ProvinceCore_Creation
    {
        public SR_ProvinceCore_Creation(
            ProtoEntity parentMapEntity,
            ProtoEntity parentCellEntity, int selfIndex,
            int[] neighbourProvinceEntities)
        {
            this.parentMapEntity = parentMapEntity;

            this.parentCellEntity = parentCellEntity;
            this.selfIndex = selfIndex;

            this.neighbourProvinceEntities = neighbourProvinceEntities;
        }

        public readonly ProtoEntity parentMapEntity;

        public readonly ProtoEntity parentCellEntity;
        public readonly int selfIndex;

        public readonly int[] neighbourProvinceEntities;
    }
}
