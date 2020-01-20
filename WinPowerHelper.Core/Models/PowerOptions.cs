namespace WinPowerHelper.Core.Models
{
    using System.ComponentModel;

    public enum PowerOptions
    {
        [Description("关机")]
        Shutdown = 0, // 关机
        [Description("休眠")]
        Sleep, // 休眠
        [Description("重启")]
        Restart // 重启
    }
}
