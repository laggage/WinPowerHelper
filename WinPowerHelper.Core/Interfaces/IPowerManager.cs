namespace WinPowerHelper.Core.Interfaces
{
    using System;

    public interface IPowerManager
    {
        /// <summary>
        /// 立刻关机
        /// </summary>
        void Shutdown();
        /// <summary>
        /// 等待<param name="interval"></param>时间后关机
        /// </summary>
        /// <param name="interval"></param>
        void Shutdown(TimeSpan interval);

        /// <summary>
        /// 让电脑立即休眠
        /// </summary>
        void Sleep();
        /// <summary>
        /// 在等待<param name="interval"></param>时间后, 立即休眠
        /// </summary>
        /// <param name="interval"></param>
        void Sleep(TimeSpan interval);

        /// <summary>
        /// 立即重启
        /// </summary>
        void Restart();
        /// <summary>
        /// 在等待<param name="interval"></param>时间后, 立即重启
        /// </summary>
        /// <param name="interval"></param>
        void Restart(TimeSpan interval);
    }
}
