
using System.Collections.Generic;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SProvinceCoreCreation : IEcsInitSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsFilterInject<Inc<CMap>> mapFilter = default;
        readonly EcsPoolInject<CMap> mapPool = default;

        readonly EcsPoolInject<CProvinceCore> pCPool = default;

        public void Init(IEcsSystems systems)
        {
            //������ PC �� ��������
            ProvincesCoreCreation();
        }

        readonly EcsFilterInject<Inc<SRProvinceCoreCreation>> pCCreationSRFilter = default;
        readonly EcsPoolInject<SRProvinceCoreCreation> pCCreationSRPool = default;
        void ProvincesCoreCreation()
        {
            //������ ��������� ������ ���������
            List<EcsPackedEntity> tempProvinces = new();

            //��� ������ �����
            foreach(int mapEntity in mapFilter.Value)
            {
                //���� �����
                ref CMap map = ref mapPool.Value.Get(mapEntity);

                //������� ��������� ������ ���������
                tempProvinces.Clear();

                //��� ������ ��������� � �������� �������� PC
                foreach (int provinceEntity in pCCreationSRFilter.Value)
                {
                    //���� ������
                    ref SRProvinceCoreCreation requestComp = ref pCCreationSRPool.Value.Get(provinceEntity);

                    //���� ��������� ����������� ������� �����
                    if(requestComp.parentMapPE.EqualsTo(map.selfPE))
                    {
                        //������ PC �� �������
                        ProvinceCoreCreation(
                            provinceEntity,
                            ref requestComp);

                        //���� PC
                        ref CProvinceCore pC = ref pCPool.Value.Get(provinceEntity);

                        //������� ��������� � ������
                        tempProvinces.Add(pC.selfPE);

                        //������� ������
                        pCCreationSRPool.Value.Del(provinceEntity);
                    }
                }

                //��������� ������ ��� ������ ��������� �����
                map.provincePEs = tempProvinces.ToArray();
            }
        }

        void ProvinceCoreCreation(
            int provinceEntity,
            ref SRProvinceCoreCreation requestComp)
        {
            //��������� �������� ��������� ��������� PC
            ref CProvinceCore pC = ref pCPool.Value.Add(provinceEntity);

            //��������� �������� ������ PC
            pC = new(
                world.Value.PackEntity(provinceEntity),
                requestComp.neighbourProvincePEs);
        }
    }
}
