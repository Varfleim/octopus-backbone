
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map
{
    public class A_Map : ProtoAspectInject
    {
        public ProtoPool<C_Map> map_P;

        public ProtoPool<SR_Map_Creation> map_Creation_SR_P;
        public ProtoIt map_Creation_SR_I = new ProtoIt(It.Inc<SR_Map_Creation>());

        public ProtoPool<C_ProvinceCore> pC_P;

        public ProtoPool<SR_ProvinceCore_Creation> pC_Creation_SR_P;
        public ProtoIt pC_Creation_SR_I = new ProtoIt(It.Inc<SR_ProvinceCore_Creation>());

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="mapEntity"></param>
        public void Map_Creation_SR(
            ProtoEntity mapEntity)
        {
            //Назначаем сущности запрос создания карты и заполняем его данные
            ref SR_Map_Creation rComp = ref map_Creation_SR_P.Add(mapEntity);
            rComp = new(0);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля представления карты
        /// </summary>
        /// <param name="parentMapEntity"></param>
        /// <param name="parentCellEntity"></param>
        /// <param name="selfIndex"></param>
        /// <param name="neighbourIndexes"></param>
        public ProtoEntity ProvinceCore_Creation_SR(
            ProtoEntity parentMapEntity,
            ProtoEntity parentCellEntity, int selfIndex,
            ref int[] neighbourIndexes)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceCore_Creation rComp = ref pC_Creation_SR_P.NewEntity(out ProtoEntity pEntity);

            //Заполняем данные запроса
            rComp = new(
                parentMapEntity,
                parentCellEntity, selfIndex,
                neighbourIndexes);

            return pEntity;
        }
    }
}
