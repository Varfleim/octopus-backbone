
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
            //Создаём PC по запросам
            ProvincesCoreCreation();
        }

        readonly EcsFilterInject<Inc<SR_ProvinceCoreCreation>> pCCreationSRFilter = default;
        readonly EcsPoolInject<SR_ProvinceCoreCreation> pCCreationSRPool = default;
        void ProvincesCoreCreation()
        {
            //Создаём временный список провинций
            List<EcsPackedEntity> tempProvinces = new();

            //Для каждой карты
            foreach(int mapEntity in mapFilter.Value)
            {
                //Берём карту
                ref C_Map map = ref mapPool.Value.Get(mapEntity);
                //Создаём временную PE карты
                EcsPackedEntity mapPE = world.Value.PackEntity(mapEntity);

                //Очищаем временный список провинций
                tempProvinces.Clear();

                //Для каждой провинции с запросом создания PC
                foreach (int provinceEntity in pCCreationSRFilter.Value)
                {
                    //Берём запрос
                    ref SR_ProvinceCoreCreation requestComp = ref pCCreationSRPool.Value.Get(provinceEntity);

                    //Если провинция принадлежит текущей карте
                    if(requestComp.parentMapPE.EqualsTo(mapPE))
                    {
                        //Создаём PC по запросу
                        ProvinceCoreCreation(
                            provinceEntity,
                            ref requestComp);

                        //Берём PC
                        ref C_ProvinceCore pC = ref pCPool.Value.Get(provinceEntity);

                        //Заносим провинцию в список
                        tempProvinces.Add(pC.selfPE);

                        //Удаляем запрос
                        pCCreationSRPool.Value.Del(provinceEntity);
                    }
                }

                //Сохраняем список как массив провинций карты
                map.provincePEs = tempProvinces.ToArray();
            }
        }

        void ProvinceCoreCreation(
            int provinceEntity,
            ref SR_ProvinceCoreCreation requestComp)
        {
            //Назначаем сущности провинции компонент PC
            ref C_ProvinceCore pC = ref pCPool.Value.Add(provinceEntity);

            //Заполняем основные данные PC
            pC = new(
                world.Value.PackEntity(provinceEntity),
                requestComp.neighbourProvincePEs);
        }
    }
}
