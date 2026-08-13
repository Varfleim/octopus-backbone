
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.ConditionalSystems;

namespace GBB.Map.Render
{
    public class MapRender_Solver : IConditionalSystemSolver
    {
        [DI] A_MapRender mapRender_A;

        public bool Solve()
        {
            //Если есть активная карта
            if(mapRender_A.activeMap_I.IsEmptySlow() == false)
            {
                return true;
            }

            return false;
        }
    }
}
