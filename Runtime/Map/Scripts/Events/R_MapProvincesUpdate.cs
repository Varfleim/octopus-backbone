
namespace GBB.Map
{
    public readonly struct R_MapProvincesUpdate
    {
        public R_MapProvincesUpdate(
            bool isMaterialUpdated, bool isHeightUpdated, bool isColorUpdated)
        {
            this.isMaterialUpdated = isMaterialUpdated;
            this.isHeightUpdated = isHeightUpdated;
            this.isColorUpdated = isColorUpdated;
        }

        public readonly bool isMaterialUpdated;
        public readonly bool isHeightUpdated ;
        public readonly bool isColorUpdated;
    }
}
