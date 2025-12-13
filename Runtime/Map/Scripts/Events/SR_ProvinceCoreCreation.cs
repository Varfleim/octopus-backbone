
namespace GBB.Map
{
    public readonly struct SR_ProvinceCoreCreation
    {
        public SR_ProvinceCoreCreation(
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
