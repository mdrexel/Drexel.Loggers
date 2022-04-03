namespace Drexel.Loggers.Results
{
    internal sealed class ValueContainer<T> : IValueContainer<T>
    {
        private readonly object sync;
        private bool hasValue;
        private T value;

        public ValueContainer()
        {
            this.sync = new object();

            this.hasValue = false;
            this.value = default!;
        }

        public bool HasValue => this.hasValue;

        public T Value => this.value;

        public bool GetValue(out T value)
        {
            lock (this.sync)
            {
                if (this.hasValue)
                {
                    value = this.value;
                    return true;
                }
                else
                {
                    value = default!;
                    return false;
                }
            }
        }

        public bool RemoveValue(out T value)
        {
            lock (this.sync)
            {
                if (this.HasValue)
                {
                    value = this.value;
                    this.hasValue = false;
                    this.value = default!;
                    return true;
                }
                else
                {
                    value = default!;
                    return false;
                }
            }
        }

        public bool SetValue(T newValue, out T oldValue)
        {
            lock (this.sync)
            {
                bool hadValue = this.HasValue;
                if (hadValue)
                {
                    oldValue = this.value;
                }
                else
                {
                    oldValue = default!;
                }

                this.value = newValue;
                this.hasValue = true;

                return hadValue;
            }
        }

        public bool TryAddValue(T value, out T currentValue)
        {
            lock (this.sync)
            {
                if (this.HasValue)
                {
                    currentValue = this.value;
                    return false;
                }
                else
                {
                    currentValue = default!;
                    this.value = value;
                    this.hasValue = true;
                    return true;
                }
            }
        }
    }
}
