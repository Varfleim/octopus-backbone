
using System.Collections.Generic;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map
{
    public class S_ProvinceCore_Creation : VFSystem, IProtoInitSystem, IProtoRunSystem
    {
        [DI] A_Map map_A;

        public void Init(IProtoSystems systems)
        {
            //Создаём PC
            PCs_Creation();
        }
        
        public void Run()
        {
            //Создаём PC
            PCs_Creation();
        }

        void PCs_Creation()
        {
            //Берём временный список из пула
            List<ProtoEntity> tempNeighbourEntities = ListPool<ProtoEntity>.Get();

            //Для каждого запроса создания PC
            foreach (ProtoEntity pEntity in map_A.pC_Creation_SR_I)
            {
                //Берём запрос и назначаем компонент PC
                ref SR_ProvinceCore_Creation rComp = ref map_A.pC_Creation_SR_P.Get(pEntity);
                ref C_ProvinceCore pC = ref map_A.pC_P.Add(pEntity);

                //Берём родительскую карту
                ref C_Map parentM = ref map_A.map_P.Get(rComp.parentMapEntity);

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
                map_A.pC_Creation_SR_P.Del(pEntity);
            }

            //Возвращаем список в пул
            ListPool<ProtoEntity>.Add(tempNeighbourEntities);
        }
    }
}
