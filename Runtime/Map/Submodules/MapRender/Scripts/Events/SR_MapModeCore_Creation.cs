
namespace GBB.Map.Render
{
    public readonly struct SR_MapModeCore_Creation
    {
        public SR_MapModeCore_Creation(
            string name,
            bool defaultMapMode)
        {
            this.name = name;

            this.defaultMapMode = defaultMapMode;
        }

        public readonly string name;

        public readonly bool defaultMapMode;
    }
}
