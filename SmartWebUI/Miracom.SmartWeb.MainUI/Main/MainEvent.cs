using System;
using System.Runtime.InteropServices;

namespace Miracom.SmartWeb.UI
{
    [Guid("E2DB8CCC-63F3-4129-AF6B-36562F2CB7D7")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ILogOutEvent
    {
        [DispId(300)]
        void LogOutClicked();

    }

    public interface IControlCOMIncoming
    {
        string TimeOut { get; }
    }

}
