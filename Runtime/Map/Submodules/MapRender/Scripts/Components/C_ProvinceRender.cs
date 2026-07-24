
using Leopotam.EcsProto;

namespace GBB.Map.Render
{
    /// <summary>
    /// Компонент, хранящий данные провинции, использующиеся для универсальной визуализации
    /// </summary>
    public struct C_ProvinceRender
    {
        public C_ProvinceRender(int a)
        {
            displayedObjectPE = new();

            thinEdgesIndex = -1;
            thickEdgesIndex = -1;

            provinceHeight = 0f;

            provinceColorIndex = -1;

            provinceGO = null;
        }

        public ProtoEntity DisplayedObjectPE 
        {
            get
            {
                return displayedObjectPE;
            }
            internal set
            {
                displayedObjectPE = value;
            }
        }
        ProtoEntity displayedObjectPE;

        public int ThinEdgesIndex
        {
            get
            {
                return thinEdgesIndex;
            }
            internal set
            {
                thinEdgesIndex = value;
            }
        }
        int thinEdgesIndex;
        
        public int ThickEdgesIndex
        {
            get
            {
                return thickEdgesIndex;
            }
            internal set
            {
                thickEdgesIndex = value;
            }
        }
        int thickEdgesIndex;

        public float ProvinceHeight
        {
            get
            {
                return provinceHeight;
            }
            internal set
            {
                provinceHeight = value;
            }
        }
        float provinceHeight;

        public int ProvinceColorIndex
        {
            get
            {
                return provinceColorIndex;
            }
            internal set
            {
                provinceColorIndex = value;
            }
        }
        int provinceColorIndex;

        public GO_Province ProvinceGO
        {
            get
            {
                return provinceGO;
            }
            internal set
            {
                provinceGO = value;
            }
        }
        GO_Province provinceGO;
    }
}
