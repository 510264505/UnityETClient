using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class CoroutineComponentAwake : AwakeSystem<CoroutineComponent, Init>
    {
        public override void Awake(CoroutineComponent self, Init mono)
        {
            self.Awake(mono);
        }
    }
    public class CoroutineComponent : Component
    {
        // 用于Mono协程
        public Init Mono { get; set; }
        public void Awake(Init mono)
        {
            this.Mono = mono;
        }
    }
}
