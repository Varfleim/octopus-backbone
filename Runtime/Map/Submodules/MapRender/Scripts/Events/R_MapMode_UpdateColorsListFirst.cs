
using System.Collections.Generic;

using UnityEngine;

namespace GBB.Map.Render
{
    public readonly struct R_MapMode_UpdateColorsListFirst
    {
        public R_MapMode_UpdateColorsListFirst(
            string coloredObjectType, 
            List<Color> objectColors)
        {
            this.coloredObjectType = coloredObjectType;
            
            this.objectColors = objectColors;
        }

        public readonly string coloredObjectType;

        public readonly List<Color> objectColors;
    }
}
