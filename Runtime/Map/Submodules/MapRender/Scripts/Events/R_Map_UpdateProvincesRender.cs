
namespace GBB.Map.Render
{
    public readonly struct R_Map_UpdateProvincesRender
    {
        public R_Map_UpdateProvincesRender(
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
