
namespace GBB.Map
{
    public struct C_Map
    {
        public C_Map(
            int parentRenderEntity)
        {
            this.parentRenderEntity = parentRenderEntity;

            provinceEntities = new int[0];
        }

        public int parentRenderEntity;

        public int[] provinceEntities;

        public int Province_Get(
            int provinceIndex)
        {
            return provinceEntities[provinceIndex];
        }

        /// <summary>
        /// Ќельз€ использовать в многопоточных системах
        /// </summary>
        /// <returns></returns>
        public int Province_GetRandom()
        {
            return Province_Get(UnityEngine.Random.Range(0, provinceEntities.Length));
        }
    }
}
