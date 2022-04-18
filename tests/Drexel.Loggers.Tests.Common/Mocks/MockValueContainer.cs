using System;
using Drexel.Loggers.Results;

namespace Drexel.Loggers.Tests.Common.Mocks
{
    public sealed class MockValueContainer<T> : IValueContainer<T>
    {
        public delegate bool GetValueDelegate(out T value);

        public delegate bool RemoveValueDelegate(out T value);

        public delegate bool SetValueDelegate(T newValue, out T oldValue);

        public delegate bool TryAddValueDelegate(T value, out T currentValue);

        public MockValueContainer()
        {
            this.Sync = new object();

            bool hasValue = false;
            T value = default!;

            this.HasValueGetter = () => hasValue;
            this.HasValueSetter = x => hasValue = x;
            this.ValueGetter = () => value;
            this.ValueSetter = x => value = x;

            this.GetValueOutFunc =
                (out T value) =>
                {
                    lock (this.Sync)
                    {
                        if (this.HasValue)
                        {
                            value = this.Value;
                            return true;
                        }
                        else
                        {
                            value = default!;
                            return false;
                        }
                    }
                };

            this.RemoveValueFunc =
                () =>
                {
                    lock (this.Sync)
                    {
                        bool hadValue = this.HasValue;
                        this.HasValue = false;
                        this.Value = default!;

                        return hadValue;
                    }
                };
            this.RemoveValueOutFunc =
                (out T value) =>
                {
                    lock (this.Sync)
                    {
                        bool hadValue = this.HasValue;

                        value = hadValue ? this.Value : default!;

                        this.HasValue = false;
                        this.Value = default!;

                        return hadValue;
                    }
                };

            this.SetValueFunc =
                (T value) =>
                {
                    lock (this.Sync)
                    {
                        bool hadValue = this.HasValue;

                        this.Value = value;
                        this.HasValue = true;

                        return hadValue;
                    }
                };
            this.SetValueOutFunc =
                (T newValue, out T oldValue) =>
                {
                    lock (this.Sync)
                    {
                        if (this.HasValue)
                        {
                            oldValue = this.Value;
                            this.Value = newValue;

                            return true;
                        }
                        else
                        {
                            oldValue = default!;
                            this.Value = newValue;
                            this.HasValue = true;

                            return false;
                        }
                    }
                };

            this.TryAddValueFunc =
                (T value) =>
                {
                    lock (this.Sync)
                    {
                        if (this.HasValue)
                        {
                            return false;
                        }
                        else
                        {
                            this.Value = value;
                            this.HasValue = true;

                            return true;
                        }
                    }
                };
            this.TryAddValueOutFunc =
                (T value, out T currentValue) =>
                {
                    lock (this.Sync)
                    {
                        if (this.HasValue)
                        {
                            currentValue = this.Value;
                            return false;
                        }
                        else
                        {
                            currentValue = default!;
                            this.Value = value;
                            this.HasValue = true;

                            return true;
                        }
                    }
                };
        }

        public bool HasValue
        {
            get => this.HasValueGetter.Invoke();
            set => this.HasValueSetter.Invoke(value);
        }

        public T Value
        {
            get => this.ValueGetter.Invoke();
            set => this.ValueSetter.Invoke(value);
        }

        /// <summary>
        /// Gets the object used for synchronization by this instance's default method implementations.
        /// </summary>
        public object Sync { get; }

        public Func<bool> HasValueGetter { get; set; }

        public Action<bool> HasValueSetter { get; set; }

        public Func<T> ValueGetter { get; set; }

        public Action<T> ValueSetter { get; set; }

        public GetValueDelegate GetValueOutFunc { get; set; }

        public Func<bool> RemoveValueFunc { get; set; }

        public RemoveValueDelegate RemoveValueOutFunc { get; set; }

        public Func<T, bool> SetValueFunc { get; set; }

        public SetValueDelegate SetValueOutFunc { get; set; }

        public Func<T, bool> TryAddValueFunc { get; set; }

        public TryAddValueDelegate TryAddValueOutFunc { get; set; }

        public bool GetValue(out T value) =>
            this.GetValueOutFunc.Invoke(out value);

        public bool RemoveValue() =>
            this.RemoveValueFunc.Invoke();

        public bool RemoveValue(out T value) =>
            this.RemoveValueOutFunc.Invoke(out value);

        public bool SetValue(T newValue) =>
            this.SetValueFunc.Invoke(newValue);

        public bool SetValue(T newValue, out T oldValue) =>
            this.SetValueOutFunc.Invoke(newValue, out oldValue);

        public bool TryAddValue(T value) =>
            this.TryAddValueFunc.Invoke(value);

        public bool TryAddValue(T value, out T currentValue) =>
            this.TryAddValueOutFunc.Invoke(value, out currentValue);
    }
}
