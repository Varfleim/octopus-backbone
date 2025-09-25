using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public struct RProvinceMapPanelSetParent
    {
        public RProvinceMapPanelSetParent(
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
