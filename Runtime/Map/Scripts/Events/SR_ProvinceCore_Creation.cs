
namespace GBB.Map
{
    public readonly struct SR_ProvinceCore_Creation
    {
        public SR_ProvinceCore_Creation(
            int parentMapEntity,
            int[] neighbourProvinceEntities)
        {
            this.parentMapEntity = parentMapEntity;

            this.neighbourProvinceEntities = neighbourProvinceEntities;
        }

        public readonly int parentMapEntity;

        public readonly int[] neighbourProvinceEntities;
    }
}
