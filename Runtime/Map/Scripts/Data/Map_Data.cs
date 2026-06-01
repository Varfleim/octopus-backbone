
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public class Map_Data : MonoBehaviour
    {
        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="mapEntity"></param>
        /// <param name="r_P"></param>
        public static void Map_Creation_SR(
            int mapEntity,
            EcsPool<SR_Map_Creation> r_P)
        {
            //Назначаем сущности запрос создания карты и заполняем его данные
            ref SR_Map_Creation requestComp = ref r_P.Add(mapEntity);
            requestComp = new(0);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля представления карты
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="r_P"></param>
        /// <param name="parentMapEntity"></param>
        /// <param name="parentCellEntity"></param>
        /// <param name="selfIndex"></param>
        /// <param name="neighbourIndexes"></param>
        public static void ProvinceCore_Creation_R(
            int pEntity,
            EcsPool<SR_ProvinceCore_Creation> r_P,
            int parentMapEntity,
            int parentCellEntity, int selfIndex,
            ref int[] neighbourIndexes)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceCore_Creation rComp = ref r_P.Add(pEntity);

            //Заполняем данные запроса
            rComp = new(
                parentMapEntity,
                parentCellEntity, selfIndex,
                neighbourIndexes);
        }
    }
}
