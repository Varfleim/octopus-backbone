
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsProto;

namespace GBB.Map.Render
{
    public readonly struct R_MapMode_UpdateColorsListSecond
    {
        public R_MapMode_UpdateColorsListSecond(
            ProtoEntity mMEntity, 
            List<Color> mapModeColors, Color defaultColor)
        {
            this.mMEntity = mMEntity;
            
            this.mapModeColors = mapModeColors;

            this.defaultColor = defaultColor;
        }

        public readonly ProtoEntity mMEntity;

        public readonly List<Color> mapModeColors;

        public readonly Color defaultColor;
    }
}
