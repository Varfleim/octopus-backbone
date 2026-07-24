using UnityEngine;

using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public struct R_ProvinceMapPanel_SetParent
    {
        public R_ProvinceMapPanel_SetParent(
            ProtoPackedEntity parentProvincePE, 
            GameObject mapPanelGO)
        {
            this.parentProvincePE = parentProvincePE;
            
            this.mapPanelGO = mapPanelGO;
        }

        public readonly ProtoPackedEntity parentProvincePE; 

        public readonly GameObject mapPanelGO;
    }
}
