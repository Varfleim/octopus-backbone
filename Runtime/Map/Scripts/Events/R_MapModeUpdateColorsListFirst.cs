
using System.Collections.Generic;

using UnityEngine;

namespace GBB.Map
{
    public readonly struct R_MapModeUpdateColorsListFirst
    {
        public R_MapModeUpdateColorsListFirst(
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
