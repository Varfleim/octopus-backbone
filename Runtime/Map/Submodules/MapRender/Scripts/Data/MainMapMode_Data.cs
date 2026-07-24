
using UnityEngine;

using Leopotam.EcsProto;

namespace GBB.Map.Render
{
    public class MainMapMode_Data : MonoBehaviour
    {
        public ProtoEntity DefaultMapModeEntity
        {
            get
            {
                return defaultMapModeEntity;
            }
            internal set
            {
                defaultMapModeEntity = value;
            }
        }
        private ProtoEntity defaultMapModeEntity;
    }
}
