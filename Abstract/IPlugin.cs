using System;

namespace Lab3
{
    public interface IPlugin
    {
        String Name{ get; }
        void OnClickEventHandler(object sender, EventArgs e);
    }
}
