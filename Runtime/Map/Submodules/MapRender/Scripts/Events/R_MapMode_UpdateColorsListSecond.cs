
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public readonly struct R_MapMode_UpdateColorsListSecond
    {
        public R_MapMode_UpdateColorsListSecond(
            EcsPackedEntity mapModePE, 
            List<Color> mapModeColors, Color defaultColor)
        {
            this.mapModePE = mapModePE;
            
            this.mapModeColors = mapModeColors;

            this.defaultColor = defaultColor;
        }

        public readonly EcsPackedEntity mapModePE;

        public readonly List<Color> mapModeColors;

        public readonly Color defaultColor;
    }
}
