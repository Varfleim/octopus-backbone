
using Leopotam.EcsLite;

namespace GBB.Map
{
    /// <summary>
    /// Компонент, хранящий базовые данные провинции
    /// </summary>
    public struct C_ProvinceCore
    {
        public C_ProvinceCore(
            int[] neighbourProvinceEntities)
        {
            this.neighbourProvinceEntities = neighbourProvinceEntities;
        }

        #region ProvinceData
        public readonly int[] neighbourProvinceEntities;
        #endregion
    }
}
