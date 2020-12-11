using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class AccountPasswordComponentAwake : AwakeSystem<AccountPasswordComponent>
    {
        public override void Awake(AccountPasswordComponent self)
        {
            self.Awake();
        }
    }
    public class AccountPasswordComponent : Component
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public void Awake()
        {
            Account = PlayerPrefs.GetString("account");
            Password = PlayerPrefs.GetString("password");
        }
        public void SaveNaviteData()
        {
            PlayerPrefs.SetString("account", Account);
            PlayerPrefs.SetString("password", Password);
        }
    }
}
