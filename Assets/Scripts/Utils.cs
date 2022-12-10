using System.Collections.Generic;

public static class Utils
{
    // https://stackoverflow.com/questions/15622622/analogue-of-pythons-defaultdict
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new()
    {
        private TValue defaultValue;

        public DefaultDictionary() : base() {}

        public DefaultDictionary(TValue defaultValue) : base() 
        {
            this.defaultValue = defaultValue;
        }
        
        public new TValue this[TKey key]
        {
            get
            {
                TValue val;
                if (!TryGetValue(key, out val))
                {
                    val = defaultValue ?? new TValue();
                    Add(key, val);
                }
                return val;
            }
            set { base[key] = value; }
        }
    }
}