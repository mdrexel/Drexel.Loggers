namespace Drexel.Loggers.Results
{
    internal sealed class ValueContainer<T> : IValueContainer<T>
    {
        private readonly object sync;

        public ValueContainer()
        {
            this.sync = new object();

            this.HasValue = false;
            this.Value = default!;
        }

        public bool HasValue { get; private set; }

        public T Value { get; private set; }

        public bool RemoveValue(out T value)
        {
            lock (this.sync)
            {
                if (this.HasValue)
                {
                    value = this.Value;
                    this.Value = default!;
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
                    oldValue = this.Value;
                }
                else
                {
                    oldValue = default!;
                }

                this.Value = newValue;
                this.HasValue = true;

                return hadValue;
            }
        }

        public bool TryAddValue(T value, out T existingValue)
        {
            lock (this.sync)
            {
                if (this.HasValue)
                {
                    existingValue = this.Value;
                    return false;
                }
                else
                {
                    existingValue = default!;
                    this.Value = value;
                    return true;
                }
            }
        }
    }
}
