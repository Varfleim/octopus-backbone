
using System.Collections.Generic;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class S_ProvinceCore_Creation : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsPoolInject<C_Map> map_P = default;

        readonly EcsPoolInject<C_ProvinceCore> pC_P = default;

        public void Init(IEcsSystems systems)
        {
            //Создаём PC
            PCs_Creation();
        }
        
        public void Run(IEcsSystems systems)
        {
            //Создаём PC
            PCs_Creation();
        }

        readonly EcsFilterInject<Inc<SR_ProvinceCore_Creation>> pC_Creation_SR_F = default;
        void PCs_Creation()
        {
            //Берём временный список из пула
            List<int> tempNeighbourEntities = ListPool<int>.Get();

            //Для каждого запроса создания PC
            foreach (int pEntity in pC_Creation_SR_F.Value)
            {
                //Берём запрос и назначаем компонент PC
                ref SR_ProvinceCore_Creation rComp = ref pC_Creation_SR_F.Pools.Inc1.Get(pEntity);
                ref C_ProvinceCore pC = ref pC_P.Value.Add(pEntity);

                //Берём родительскую карту
                ref C_Map parentM = ref map_P.Value.Get(rComp.parentMapEntity);

                //Очищаем временный список
                tempNeighbourEntities.Clear();

                //Для каждого индекса соседа
                for(int a = 0; a < rComp.neighbourProvinceEntities.Length; a++)
                {
                    //Заносим сущность соседа во временный список
                    tempNeighbourEntities.Add(parentM.provinceEntities[rComp.neighbourProvinceEntities[a]]);
                }

                //Заполняем основные данные PC
                pC = new(
                    rComp.parentCellEntity,
                    tempNeighbourEntities.ToArray());

                //Удаляем запрос
                pC_Creation_SR_F.Pools.Inc1.Del(pEntity);
            }

            //Возвращаем список в пул
            ListPool<int>.Add(tempNeighbourEntities);
        }
    }
}
