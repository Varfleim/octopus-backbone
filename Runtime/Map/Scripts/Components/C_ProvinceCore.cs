
using Leopotam.EcsLite;

namespace GBB.Map
{
    /// <summary>
    /// Компонент, хранящий базовые данные провинции
    /// </summary>
    public struct C_ProvinceCore
    {
        public C_ProvinceCore(
            int parentCellEntity,
            int[] neighbourProvinceEntities)
        {
            this.parentCellEntity = parentCellEntity;

            this.neighbourProvinceEntities = neighbourProvinceEntities;
        }

        public readonly int parentCellEntity;

        #region ProvinceData
        public readonly int[] neighbourProvinceEntities;
        #endregion
    }
}
