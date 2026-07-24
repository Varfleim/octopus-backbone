
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public class A_Input : ProtoAspectInject
    {
        public ProtoPool<R_Mouse_PositionChange> mouse_PositionChange_R_P;
        public ProtoIt mouse_PositionChange_I = new(It.Inc<R_Mouse_PositionChange>());
        public ProtoPool<R_Mouse_MapPositionCheck> mouse_MapPositionCheck_R_P;
        public ProtoIt mouse_MapPositionCheck_I = new(It.Inc<R_Mouse_MapPositionCheck>());
        public ProtoPool<R_Mouse_MapClickCheck> mouse_MapClickCheck_R_P;
        public ProtoIt mouse_MapClickCheck_I = new(It.Inc<R_Mouse_MapClickCheck>());

        public ProtoPool<R_Camera_Moving> camera_Moving_R_P;
        public ProtoIt camera_Moving_R_I = new(It.Inc<R_Camera_Moving>());

        public void Mouse_MapPositionCheck_Request(
            ProtoPackedEntity currentProvincePE)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_Mouse_MapPositionCheck rComp = ref mouse_MapPositionCheck_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                currentProvincePE);
        }

        public void Mouse_MapClickCheck_Request(
            ProtoPackedEntity currentProvincePE,
            bool leftMouseButtonClick, bool rightMouseButtonClick)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_Mouse_MapClickCheck rComp = ref mouse_MapClickCheck_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                currentProvincePE,
                leftMouseButtonClick, rightMouseButtonClick);
        }
    }
}
