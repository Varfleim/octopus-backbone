
namespace GBB.Map
{
    public readonly struct SR_ProvinceCore_Creation
    {
        public SR_ProvinceCore_Creation(
            int parentMapEntity,
            int parentCellEntity, int selfIndex,
            int[] neighbourProvinceEntities)
        {
            this.parentMapEntity = parentMapEntity;

            this.parentCellEntity = parentCellEntity;
            this.selfIndex = selfIndex;

            this.neighbourProvinceEntities = neighbourProvinceEntities;
        }

        public readonly int parentMapEntity;

        public readonly int parentCellEntity;
        public readonly int selfIndex;

        public readonly int[] neighbourProvinceEntities;
    }
}
