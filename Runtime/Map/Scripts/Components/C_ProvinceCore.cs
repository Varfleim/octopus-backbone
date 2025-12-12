
using Leopotam.EcsLite;

namespace GBB.Map
{
    /// <summary>
    /// Компонент, хранящий базовые данные провинции
    /// </summary>
    public struct C_ProvinceCore
    {
        public C_ProvinceCore(
            EcsPackedEntity[] neighbourProvincePEs)
        {
            this.neighbourProvincePEs = neighbourProvincePEs;
        }

        #region ProvinceData
        public readonly EcsPackedEntity[] neighbourProvincePEs;
        #endregion
    }
}
