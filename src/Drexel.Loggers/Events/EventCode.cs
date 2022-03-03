using System;
using System.Diagnostics;
using static System.FormattableString;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a globally unique identifier event code.
    /// </summary>
    [DebuggerDisplay("{ToStringDebug(),nq}")]
    public sealed class EventCode : IEquatable<EventCode>, IComparable<EventCode>
    {
        private readonly string? debugHumanReadableValue;
        private readonly string stringRepresentation;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCode"/> class.
        /// </summary>
        /// <param name="group">
        /// The group the event code belongs to.
        /// </param>
        /// <param name="value">
        /// The value of the event code.
        /// </param>
        /// <param name="debugHumanReadableValue">
        /// An optional human-readable name displayed when debugging.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="group"/> already contains an event code with the specified
        /// <paramref name="value"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="group"/> is <see langword="null"/>.
        /// </exception>
        public EventCode(
            EventCodeGroup group,
            ushort value,
            string? debugHumanReadableValue = null)
        {
            this.Group = group ?? throw new ArgumentNullException(nameof(group));
            this.Value = value;
            this.debugHumanReadableValue = debugHumanReadableValue;

            // `TryAdd` is documented as throwing `OverflowException` when the `ConcurrentDictionary` cannot hold any
            // more items (the limit is hard-coded at `int32.MaxValue`), but because `value` is `ushort`, we cannot
            // actually reach this limit. So, `OverflowException` cannot escape the constructor.
            if (!group.MutableEventCodes.TryAdd(value, this))
            {
                throw new ArgumentException(
                    "The specified event code group already contains an event code with the specified value.",
                    nameof(value));
            }

            this.stringRepresentation = Invariant($"{group.Id}-{value}");
        }

        /// <summary>
        /// Gets the group associated with this event code.
        /// </summary>
        public EventCodeGroup Group { get; }

        /// <summary>
        /// Gets the value of this event code.
        /// </summary>
        public ushort Value { get; }

        public static bool operator ==(EventCode? left, EventCode? right)
        {
            if (left is null)
            {
                return right is null;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(EventCode? left, EventCode? right)
        {
            if (left is null)
            {
                return !(right is null);
            }
            else
            {
                return !left.Equals(right);
            }
        }

        public int CompareTo(EventCode? other)
        {
            if (other is null)
            {
                // By convention, `null` is expected to appear first.
                return 1;
            }
            else
            {
                // Sort first by group; then, within a group, sort by value.
                int buffer = this.Group.CompareTo(other.Group);
                if (buffer == 0)
                {
                    return this.Value.CompareTo(other.Value);
                }
                else
                {
                    return buffer;
                }
            }
        }

        public bool Equals(EventCode? other)
        {
            if (other is null)
            {
                return false;
            }
            else
            {
                return this.Group == other.Group
                    && this.Value == other.Value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is EventCode other)
            {
                return this.Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => unchecked((((int)this.Group.Id) << 16) | this.Value);

        public override string ToString() => this.stringRepresentation;

        private string ToStringDebug()
        {
            if (this.debugHumanReadableValue is null)
            {
                return Invariant($"({this.stringRepresentation})");
            }
            else
            {
                return Invariant($"({this.stringRepresentation}) {this.debugHumanReadableValue}");
            }
        }
    }
}
