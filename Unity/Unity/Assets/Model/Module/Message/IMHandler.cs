using System;

namespace ETModel
{
	public interface IMHandler
	{
		void Handle(Session session, object message);
		Type GetMessageType();
	}
    public interface IMHandlerEx
    {
        void Handle(object message);
        Type GetMessageType();
    }
}