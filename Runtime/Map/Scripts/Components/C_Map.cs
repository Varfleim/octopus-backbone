
using Leopotam.EcsLite;

namespace GBB.Map
{
    public struct C_Map
    {
        public C_Map(
            EcsPackedEntity selfPE, string selfName)
        {
            this.selfPE = selfPE;
            this.selfName = selfName;
            
            provincePEs = new EcsPackedEntity[0];
        }

        public readonly EcsPackedEntity selfPE;
        public readonly string selfName;

        public EcsPackedEntity[] provincePEs;

        public EcsPackedEntity GetProvince(
            int provinceIndex)
        {
            return provincePEs[provinceIndex];
        }

        /// <summary>
        /// ������ ������������ � ������������� ��������
        /// </summary>
        /// <returns></returns>
        public EcsPackedEntity GetProvinceRandom()
        {
            return GetProvince(UnityEngine.Random.Range(0, provincePEs.Length));
        }
    }
}
