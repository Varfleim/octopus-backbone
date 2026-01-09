
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
        /// <param name="sR_P"></param>
        /// <param name="mapEntity"></param>
        /// <param name="mapName"></param>
        public static void Map_Creation_SelfRequest(
            EcsPool<SR_Map_Creation> sR_P,
            int mapEntity,
            string mapName)
        {
            //Назначаем сущности запрос создания карты и заполняем его данные
            ref SR_Map_Creation requestComp = ref sR_P.Add(mapEntity);
            requestComp = new(mapName);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля представления карты
        /// </summary>
        /// <param name="sR_P"></param>
        /// <param name="provinceEntity"></param>
        /// <param name="parentMapPE"></param>
        /// <param name="neighbours"></param>
        public static void ProvinceCore_Creation_Request(
            EcsPool<SR_ProvinceCore_Creation> sR_P,
            int provinceEntity,
            int parentMapEntity,
            List<int> neighbours)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceCore_Creation requestComp = ref sR_P.Add(provinceEntity);

            //Заполняем данные запроса
            requestComp = new(
                parentMapEntity,
                neighbours.ToArray());
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля представления карты
        /// Требуемый фильтр - Inc<SR_ProvinceCore_Creation>
        /// </summary>
        /// <param name="map"></param>
        /// <param name="pC_Creation_SR_F"></param>
        public static void ProvincesCore_Creation(
            ref C_Map map,
            EcsFilter pC_Creation_SR_F, EcsPool<SR_ProvinceCore_Creation> pC_Creation_SR_P,
            EcsPool<C_ProvinceCore> pC_P)
        {
            //Берём временный список из пула
            List<int> tempProvinceEntities = ListPool<int>.Get();

            //Для каждой провинции с запросом создания PC
            foreach (int provinceEntity in pC_Creation_SR_F)
            {
                //Берём запрос
                ref SR_ProvinceCore_Creation requestComp = ref pC_Creation_SR_P.Get(provinceEntity);

                //Создаём PC по запросу
                ProvinceCore_Creation(
                    ref requestComp,
                    provinceEntity,
                    pC_P);

                //Заносим провинцию во временный список
                tempProvinceEntities.Add(provinceEntity);

                //Удаляем запрос
                pC_Creation_SR_P.Del(provinceEntity);
            }

            //Сохраняем список как массив провинций карты
            map.provinceEntities = tempProvinceEntities.ToArray();

            //Возвращаем список в пул
            ListPool<int>.Add(tempProvinceEntities);
        }

        /// <summary>
        /// Закрытая функция, поскольку запрашивается только косвенно
        /// </summary>
        /// <param name="requestComp"></param>
        /// <param name="provinceEntity"></param>
        /// <param name="pC_P"></param>
        private static void ProvinceCore_Creation(
            ref SR_ProvinceCore_Creation requestComp,
            int provinceEntity,
            EcsPool<C_ProvinceCore> pC_P)
        {
            //Назначаем сущности компонент PC
            ref C_ProvinceCore pC = ref pC_P.Add(provinceEntity);

            //Заполняем основные данные PC
            pC = new(
                requestComp.neighbourProvinceEntities);
        }
    }
}
