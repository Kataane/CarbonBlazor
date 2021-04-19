using System;
using System.Collections.Generic;
using System.Linq;

namespace CarbonBlazor.Utilities
{
    public class ClassMapper
    {
        public string Class => AsString();

        public string BaseClass { get; set; }

        private readonly Dictionary<Func<string>, Func<bool>> _map = new Dictionary<Func<string>, Func<bool>>();

        private readonly Func<bool> _true = (() => true);

        public ClassMapper() {}

        public ClassMapper(string baseClass)
        {
            BaseClass = baseClass;
        }

        public ClassMapper SetBase(string value) 
        {
            BaseClass = value;
            return this;
        }

        public ClassMapper Add(string value) => Add(() => value, _true);

        public ClassMapper Add(Func<string> func) => Add(func, _true);

        public ClassMapper Add(string value, Func<bool> condition) => Add(() => value, condition); 

        public ClassMapper Add(Func<string> func, Func<bool> condition)
        {
            _map.Add(func, condition);
            return this;
        }

        public ClassMapper Add(string value, bool condition)
        {
            _map.Add(() => value, () => condition);
            return this;
        }

        public ClassMapper Brake(string value, bool condition)
        {
            if (!condition) return this;

            return Clear().Add(value);
        }

        public ClassMapper Clear()
        {
            _map.Clear();

            return Add(() => BaseClass, () => !string.IsNullOrEmpty(BaseClass));
        }

        public override string ToString()
        {
            return AsString();
        }

        public string AsString()
        {
            return string.Join(" ", _map.Where(i => i.Value()).Select(i => i.Key()));
        }
    }

}