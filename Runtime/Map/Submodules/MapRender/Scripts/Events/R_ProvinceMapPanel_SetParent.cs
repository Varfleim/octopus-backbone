using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public struct R_ProvinceMapPanel_SetParent
    {
        public R_ProvinceMapPanel_SetParent(
            EcsPackedEntity parentProvincePE, 
            GameObject mapPanelGO)
        {
            this.parentProvincePE = parentProvincePE;
            
            this.mapPanelGO = mapPanelGO;
        }

        public readonly EcsPackedEntity parentProvincePE; 

        public readonly GameObject mapPanelGO;
    }
}
