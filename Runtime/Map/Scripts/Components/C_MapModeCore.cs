
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public struct C_MapModeCore
    {
        public C_MapModeCore(
            string selfName)
        {
            this.selfName = selfName;

            colors = new();
            defaultColor = new Color();
        }

        public readonly string selfName;

        public readonly List<Color> colors;
        public Color defaultColor;

        public Color GetProvinceColor(
            ref C_ProvinceRender pR)
        {
            if(pR.ProvinceColorIndex > -1)
            {
                return colors[pR.ProvinceColorIndex];
            }
            else
            {
                return defaultColor;
            }
        }
    }
}
