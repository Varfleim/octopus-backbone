using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public struct R_ProvinceMapPanelSetParent
    {
        public R_ProvinceMapPanelSetParent(
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
