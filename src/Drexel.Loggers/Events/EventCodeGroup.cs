﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a logical group of event codes.
    /// </summary>
    [DebuggerDisplay("{ToStringDebug(),nq}")]
    public sealed class EventCodeGroup : IEquatable<EventCodeGroup>, IComparable<EventCodeGroup>
    {
        private static readonly ConcurrentDictionary<ushort, EventCodeGroup> Existing;

        private readonly string? debugHumanReadableName;

        static EventCodeGroup()
        {
            EventCodeGroup.Existing =
                new ConcurrentDictionary<ushort, EventCodeGroup>();
            EventCodeGroup.Groups =
                new ReadOnlyCollectionCollectionAdapter<EventCodeGroup>(EventCodeGroup.Existing.Values);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCodeGroup"/> class.
        /// </summary>
        /// <param name="id">
        /// The ID of the event code group. Event code group IDs must be unique.
        /// </param>
        /// <param name="debugHumanReadableName">
        /// An optional human-readable name displayed when debugging.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="id"/> has already been used by an instance of <see cref="EventCodeGroup"/>.
        /// </exception>
        public EventCodeGroup(
            ushort id,
            string? debugHumanReadableName = null)
        {
            this.Id = id;
            this.debugHumanReadableName = debugHumanReadableName;

            // `TryAdd` is documented as throwing `OverflowException` when the `ConcurrentDictionary` cannot hold any
            // more items (the limit is hard-coded at `int32.MaxValue`), but because `id` is `ushort`, we cannot
            // actually reach this limit. So, `OverflowException` cannot escape the constructor.
            if (!EventCodeGroup.Existing.TryAdd(Id, this))
            {
                throw new ArgumentException(
                    "An event code group with the specified ID has already been created.",
                    nameof(id));
            }

            this.MutableEventCodes = new ConcurrentDictionary<ushort, EventCode>();
            this.EventCodes = new ReadOnlyCollectionCollectionAdapter<EventCode>(this.MutableEventCodes.Values);
        }

        /// <summary>
        /// Gets the event code groups.
        /// </summary>
        public static IReadOnlyCollection<EventCodeGroup> Groups { get; }

        /// <summary>
        /// Gets the ID of this event code group.
        /// </summary>
        public ushort Id { get; }

        /// <summary>
        /// Gets the event codes that have been instantiated using this group.
        /// </summary>
        public IReadOnlyCollection<EventCode> EventCodes { get; }

        /// <summary>
        /// Gets the event codes that have been instantiated using this group by value as a mutable collection.
        /// </summary>
        internal ConcurrentDictionary<ushort, EventCode> MutableEventCodes { get; }

        /// <inheritdoc/>
        public static bool operator ==(EventCodeGroup? left, EventCodeGroup? right)
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

        /// <inheritdoc/>
        public static bool operator !=(EventCodeGroup? left, EventCodeGroup? right)
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

        internal static EventCodeGroup GetOrAdd(ushort id, string? debugHumanReadableName = null) =>
            EventCodeGroup.Existing.GetOrAdd(id, _ => new EventCodeGroup(id, debugHumanReadableName));

        /// <inheritdoc/>
        public int CompareTo(EventCodeGroup other)
        {
            if (other is null)
            {
                // By convention, `null` is expected to appear first.
                return 1;
            }
            else
            {
                return this.Id.CompareTo(other.Id);
            }
        }

        /// <inheritdoc/>
        public bool Equals(EventCodeGroup? other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is EventCodeGroup other)
            {
                return this.Equals(other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the hash code of the event code group.
        /// </summary>
        /// <returns>
        /// The hash code of the event code group.
        /// </returns>
        public override int GetHashCode() => this.Id;

        /// <inheritdoc/>
        public override string ToString() => this.Id.ToString(CultureInfo.InvariantCulture);

        private string ToStringDebug()
        {
            if (this.debugHumanReadableName is null)
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "({0})",
                    this.Id);
            }
            else
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "({0}) {1}",
                    this.Id,
                    this.debugHumanReadableName);
            }
        }
    }
}
