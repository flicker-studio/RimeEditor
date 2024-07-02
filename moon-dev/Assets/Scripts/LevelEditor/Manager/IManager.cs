using Cysharp.Threading.Tasks;

namespace LevelEditor
{
    /// <summary>
    /// Manager component interface
    /// </summary>
    public interface IManager
    {
        public UniTask Initialization();
    }
}