
using Leopotam.EcsProto;

namespace GBB.Map
{
    /// <summary>
    /// Компонент, хранящий базовые данные провинции
    /// </summary>
    public struct C_ProvinceCore
    {
        public C_ProvinceCore(
            ProtoEntity parentCellEntity,
            ProtoEntity[] neighbourProvinceEntities)
        {
            this.parentCellEntity = parentCellEntity;

            this.neighbourProvinceEntities = neighbourProvinceEntities;
        }

        public readonly ProtoEntity parentCellEntity;

        #region ProvinceData
        public readonly ProtoEntity[] neighbourProvinceEntities;
        #endregion
    }
}
