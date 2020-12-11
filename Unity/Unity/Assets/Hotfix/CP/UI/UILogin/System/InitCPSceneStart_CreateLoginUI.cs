using ETModel;

namespace ETHotfix
{
    [Event(CPEventIdType.InitScensStart)]
    public class InitCPSceneStart_CreateLoginUI : AEvent
    {
        public override void Run()
        {
            //UI ui = UICPCanvasFactory.Create<UICPLoginComponent>(CPABName.Prefab, CPUIType.UILoginCanvas);
            //Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
