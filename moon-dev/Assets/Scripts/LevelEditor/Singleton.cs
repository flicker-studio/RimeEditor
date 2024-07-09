namespace LevelEditor
{
    /// <summary>
    ///     Singleton Pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;

        /// <summary>
        ///     The instance of a singleton class
        /// </summary>
        public static T Instance
        {
            get { return _instance ??= new T(); }
        }
    }
}