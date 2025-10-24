
using System.Collections.Generic;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SProvinceCoreCreation : IEcsInitSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsFilterInject<Inc<C_Map>> mapFilter = default;
        readonly EcsPoolInject<C_Map> mapPool = default;

        readonly EcsPoolInject<C_ProvinceCore> pCPool = default;

        public void Init(IEcsSystems systems)
        {
            //������ PC �� ��������
            ProvincesCoreCreation();
        }

        readonly EcsFilterInject<Inc<SR_ProvinceCoreCreation>> pCCreationSRFilter = default;
        readonly EcsPoolInject<SR_ProvinceCoreCreation> pCCreationSRPool = default;
        void ProvincesCoreCreation()
        {
            //������ ��������� ������ ���������
            List<EcsPackedEntity> tempProvinces = new();

            //��� ������ �����
            foreach(int mapEntity in mapFilter.Value)
            {
                //���� �����
                ref C_Map map = ref mapPool.Value.Get(mapEntity);

                //������� ��������� ������ ���������
                tempProvinces.Clear();

                //��� ������ ��������� � �������� �������� PC
                foreach (int provinceEntity in pCCreationSRFilter.Value)
                {
                    //���� ������
                    ref SR_ProvinceCoreCreation requestComp = ref pCCreationSRPool.Value.Get(provinceEntity);

                    //���� ��������� ����������� ������� �����
                    if(requestComp.parentMapPE.EqualsTo(map.selfPE))
                    {
                        //������ PC �� �������
                        ProvinceCoreCreation(
                            provinceEntity,
                            ref requestComp);

                        //���� PC
                        ref C_ProvinceCore pC = ref pCPool.Value.Get(provinceEntity);

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
            ref SR_ProvinceCoreCreation requestComp)
        {
            //��������� �������� ��������� ��������� PC
            ref C_ProvinceCore pC = ref pCPool.Value.Add(provinceEntity);

            //��������� �������� ������ PC
            pC = new(
                world.Value.PackEntity(provinceEntity),
                requestComp.neighbourProvincePEs);
        }
    }
}
