using ETModel;

namespace ETHotfix
{
    [Event(CPEventIdType.CreateMessageUI)]
    public class CP_CreateUIMessageBoxFactory : AEvent
    {
        public override void Run()
        {
            UI ui = UICPCanvasFactory.Create<UIMessageBoxComponent>(CPABName.Prefab, CPUIType.UIMessageCanvas);
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
