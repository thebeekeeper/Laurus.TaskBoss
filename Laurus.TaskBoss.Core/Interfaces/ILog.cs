
namespace Laurus.TaskBoss.Core.Interfaces
{
    public interface ILog
    {
        void Debug(string message, params object[] items);
        void Info(string message, params object[] items);
        void Error(string message, params object[] items);
        void Fatal(string message, params object[] items);
    }
}
