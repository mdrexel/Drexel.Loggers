using System.Collections.Generic;
using System.Globalization;

namespace Drexel.Loggers.Tests.Common
{
    /// <summary>
    /// An implementation of the <see cref="LocalizableString{TDerived}"/> abstract class.
    /// </summary>
    public class LocalizableStringImpl : LocalizableString<LocalizableStringImpl>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableStringImpl"/> class.
        /// </summary>
        /// <inheritdoc/>
        public LocalizableStringImpl(string invariant)
            : base(invariant)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableStringImpl"/> class.
        /// </summary>
        /// <inheritdoc/>
        public LocalizableStringImpl(string localization, CultureInfo culture)
            : base(localization, culture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableStringImpl"/> class.
        /// </summary>
        /// <inheritdoc/>
        public LocalizableStringImpl(
            IReadOnlyDictionary<CultureInfo, string> localizations,
            CultureInfo preferredCulture)
            : base(localizations, preferredCulture)
        {
        }

        /// <summary>
        /// Gets or sets the preferred culture of this instance.
        /// </summary>
        public new CultureInfo PreferredCulture
        {
            get => base.PreferredCulture;
            set => base.PreferredCulture = value;
        }

        /// <summary>
        /// Gets or sets the localizations of this instance.
        /// </summary>
        public new IReadOnlyDictionary<CultureInfo, string> Localizations
        {
            get => base.Localizations;
            set => base.Localizations = value;
        }
    }
}
