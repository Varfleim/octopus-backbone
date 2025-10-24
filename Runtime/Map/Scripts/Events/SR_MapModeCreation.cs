
namespace GBB.Map
{
    public readonly struct SR_MapModeCreation
    {
        public SR_MapModeCreation(
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
