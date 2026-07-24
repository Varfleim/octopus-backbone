
namespace GBB
{
    internal class GameBackbone_Module : GameModule
    {
        public override void Initialization()
        {
            mainAspect = new A_MainGameBackbone();
        }
    }
}
