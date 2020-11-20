using System;

namespace WeETL.Databases
{
    public interface IDatabaseSettings<T>
    {
        public T CreateSettings();

        public string DatabaseName { get; set; }
    }

    public abstract class AbstractDatabaseSettings<T> : IDatabaseSettings<T>
    {
        private Action<T> _onCreate;
        public string DatabaseName { get  ; set ; }

        public T CreateSettings()
        {
            T settings = InternalCreateSettings();
            _onCreate?.Invoke(settings);
            return settings;
        }
        public void OnCreate(Action<T> fn)
        {
            this._onCreate = fn;
        }
        protected abstract T InternalCreateSettings();
    }
}
