
using Leopotam.EcsLite;

namespace GBB.Map
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

        public EcsPackedEntity DisplayedObjectPE 
        {
            get
            {
                return displayedObjectPE;
            }
        }
        EcsPackedEntity displayedObjectPE;

        public int ThinEdgesIndex
        {
            get
            {
                return thinEdgesIndex;
            }
        }
        int thinEdgesIndex;
        
        public int ThickEdgesIndex
        {
            get
            {
                return thickEdgesIndex;
            }
        }
        int thickEdgesIndex;

        public float ProvinceHeight
        {
            get
            {
                return provinceHeight;
            }
        }
        float provinceHeight;

        public int ProvinceColorIndex
        {
            get
            {
                return provinceColorIndex;
            }
        }
        int provinceColorIndex;

        public GO_Province ProvinceGO
        {
            get
            {
                return provinceGO;
            }
        }
        GO_Province provinceGO;

        internal void SetDisplayedObject(
            EcsPackedEntity displayedObjectPE)
        {
            this.displayedObjectPE = displayedObjectPE;
        }

        internal void SetThinEdgesIndex(
            int thinEdgesIndex)
        {
            this.thinEdgesIndex = thinEdgesIndex;
        }
        
        internal void SetThickEdgesIndex(
            int thickEdgesIndex)
        {
            this.thickEdgesIndex = thickEdgesIndex;
        }

        internal void SetHeight(
            float provinceHeight)
        {
            this.provinceHeight = provinceHeight;
        }

        internal void SetColorIndex(
            int provinceColorIndex)
        {
            this.provinceColorIndex = provinceColorIndex;
        }

        internal void SetGO(
            GO_Province provinceGO)
        {
            this.provinceGO = provinceGO;
        }
    }
}
