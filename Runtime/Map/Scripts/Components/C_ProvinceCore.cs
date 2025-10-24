
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    /// <summary>
    /// ���������, �������� ������� ������ ���������
    /// </summary>
    public struct C_ProvinceCore
    {
        public C_ProvinceCore(
            EcsPackedEntity selfPE,
            EcsPackedEntity[] neighbourProvincePEs)
        {
            this.selfPE = selfPE;

            this.neighbourProvincePEs = neighbourProvincePEs;
        }

        public readonly EcsPackedEntity selfPE;

        #region ProvinceData
        public readonly EcsPackedEntity[] neighbourProvincePEs;
        #endregion
    }
}
