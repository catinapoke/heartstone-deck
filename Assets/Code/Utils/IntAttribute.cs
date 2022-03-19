using System;

namespace Utils
{
    public class IntAttribute
    {
        private int _value;
    
        public Action<int, int> OnChange;
    
        public static implicit operator int(IntAttribute item) => item._value;
        public static implicit operator IntAttribute(int value) => new IntAttribute(value);
        public int Value
        {
            get => _value;
            set
            {
                int old = _value;
                _value = value;
                OnChange?.Invoke(old, _value);
            }
        }

        public IntAttribute(int value)
        {
            _value = value;
        }
    }
}