using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class UICPCanvasFactory
    {
        public static UI Create<T>(string abName, string prefab) where T : Component , new()
        {
            ResourcesComponent res = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            res.LoadBundle(abName);
            GameObject ab = (GameObject)res.GetAsset(abName, prefab);
            GameObject gameObject = UnityEngine.Object.Instantiate(ab);
            //res.UnloadBundle(abName);

            UI ui = ComponentFactory.Create<UI, string, GameObject>(prefab, gameObject);
            ui.AddComponent<T>();
            return ui;
        }
    }
}
