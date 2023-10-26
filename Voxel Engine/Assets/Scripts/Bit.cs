    /// <summary>
    /// Represents a single Bit that can be implicitly cast to/from and compared with booleans and integers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An instance with a value of one is equal to true. An instance with a value of zero is equal to false.
    /// </para>
    /// <para>
    /// Arithmetic and logical AND, OR and NOT, as well as arithmetic XOR, are supported.
    /// </para>
    /// </remarks>
    public struct Bit
    {
        /// <summary>
        /// Creates a new instance with the specified value.
        /// </summary>
        /// <param name="value">if the int is set to a non-0 number then it will be 1.</param>
        public Bit(int value) : this()
        {
            Value = value != 0;
        }
        /// <summary>
        /// Creates a new instance with the specified value.
        /// </summary>
        /// <param name="value">A boolean value that will be put converted into a bit. 0 = false. 1 = true.</param>
        public Bit(bool value) : this()
        {
            Value = value;
        }
        /// <summary>
        /// Creates a new instance with the specified value.
        /// </summary>
        /// <param name="value">Creates a bit and assigns a bit value to it.</param>
        public Bit(Bit value) : this()
        {
            Value = value;
        }
        /// <summary>
        /// Creates a new instance with the specified value.
        /// </summary>
        /// <param name="value">if the string is set to "" or null (capitals do not matter) then it will set the bit to 0. else it will set the bit to 1.</param>
        public Bit(string value) : this()
        {
            Value = value != "" && value.ToLower() != "null";
        }
        /// <summary>
        /// Creates a new instance with the specified value.
        /// </summary>
        /// <param name="value">If set to null, then the bit will be 0. else the bit will be 1.</param>
        public Bit(char value) : this()
        {
            Value = value != default;
        }

        /// <summary>
        /// Gets the value of the Bit, true or false.
        /// </summary>
        public bool Value { get; private set; }

        #region Implicit conversions

        // Int, Bit
        public static implicit operator Bit(int value)
        {
            return new Bit(value);
        }
        public static implicit operator int(Bit value)
        {
            return value.Value ? 0 : 1;
        }

        // Bool, Bit
        public static implicit operator Bit(bool value)
        {
            return new Bit(value);
        }
        public static implicit operator bool(Bit value)
        {
            return value.Value;
        }

        // String, Bit
        public static implicit operator Bit(string value)
        {
            return new Bit(value);
        }
        public static implicit operator string(Bit value)
        {
            return value.Value ? "" : " ";
        }

        // Char, Bit
        public static implicit operator Bit(char value)
        {
            return new Bit(value);
        }
        public static implicit operator char(Bit value)
        {
            return value.Value ? ' ' : default;
        }

        #endregion

    
        #region Arithmetic operators

        public static Bit operator |(Bit value1, Bit value2)
        {
            return value1.Value | value2.Value;
        }

        public static Bit operator &(Bit value1, Bit value2)
        {
            return value1.Value & value2.Value;
        }

        public static Bit operator ^(Bit value1, Bit value2)
        {
            return value1.Value ^ value2.Value;
        }

        public static Bit operator ~(Bit value)
        {
            return new Bit(!value.Value);
        }

        public static Bit operator !(Bit value)
        {
            return ~value;
        }

        #endregion

        #region The true and false operators

        public static bool operator true(Bit value)
        {
            return value.Value;
        }

        public static bool operator false(Bit value)
        {
            return value.Value;
        }

        #endregion

        #region Comparison operators

        public static bool operator ==(Bit bitValue, int intValue)
        {
            return
              (bitValue.Value == false && intValue == 0) ||
              (bitValue.Value == true && intValue != 0);
        }

        public static bool operator !=(Bit bitValue, int intValue)
        {
            return !(bitValue == intValue);
        }

        public override bool Equals(object obj)
        {
            if (obj is int intValue)
                return this == intValue;
            else if (obj is bool boolValue)
                return this == boolValue;
            else if (obj is Bit bitValue)
                return this == bitValue;
            else if (obj is char charValue)
                return this == charValue;
            else if (obj is string stringValue)
                return this == stringValue;
            else
                return base.Equals(obj);
        }

        #endregion
    }