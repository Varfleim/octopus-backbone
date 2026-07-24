
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map
{
    public class S_Map_Creation : IProtoInitSystem, IProtoRunSystem
    {
        [DI] A_Map map_A;

        public void Init(IProtoSystems systems)
        {
            //Создаём карты
            Maps_Creation();
        }

        public void Run()
        {
            //Создаём карты
            Maps_Creation();
        }

        void Maps_Creation()
        {
            //Для каждого запроса создания карты
            foreach(ProtoEntity mEntity in map_A.map_Creation_SR_I)
            {
                //Берём запрос
                ref SR_Map_Creation rComp = ref map_A.map_Creation_SR_P.Get(mEntity);

                //Создаём карту
                Map_Creation(
                    mEntity,
                    ref rComp);

                //Удаляем запрос
                map_A.map_Creation_SR_P.Del(mEntity);
            }
        }

        void Map_Creation(
            ProtoEntity mEntity,
            ref SR_Map_Creation rComp)
        {
            //Назначаем переданной сущности компонент карты
            ref C_Map m = ref map_A.map_P.Add(mEntity);

            //Заполняем основные данные карты
            m = new(new());
        }
    }
}
