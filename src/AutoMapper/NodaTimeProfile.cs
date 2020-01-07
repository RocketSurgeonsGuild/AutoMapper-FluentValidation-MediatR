using System;
using AutoMapper;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper.Converters;

namespace Rocket.Surgery.Extensions.AutoMapper
{
    /// <summary>
    /// NodaTimeProfile.
    /// Implements the <see cref="Profile" />
    /// </summary>
    /// <seealso cref="Profile" />
    public class NodaTimeProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodaTimeProfile" /> class.
        /// </summary>
        public NodaTimeProfile()
        {
            CreateMappingsForDurationConverter();
            CreateMappingsForInstantConvertor();
            CreateMappingsForLocalDateConverter();
            CreateMappingsForLocalDateTimeConverter();
            CreateMappingsForLocalTimeConverter();
            CreateMappingsForOffsetConverter();
            CreateMappingsForOffsetDateTimeConverter();
            CreateMappingsForPeriodConverter();
        }

        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>The name of the profile.</value>
        public override string ProfileName => nameof(NodaTimeProfile);

        private void CreateMappingsForDurationConverter()
        {
            CreateMap<Duration, TimeSpan>().ConvertUsing<DurationConverter>();
            CreateMap<Duration?, TimeSpan?>().ConvertUsing<DurationConverter>();
            CreateMap<TimeSpan, Duration>().ConvertUsing<DurationConverter>();
            CreateMap<TimeSpan?, Duration?>().ConvertUsing<DurationConverter>();
            CreateMap<Duration, long>().ConvertUsing<DurationConverter>();
            CreateMap<Duration?, long?>().ConvertUsing<DurationConverter>();
            CreateMap<long, Duration>().ConvertUsing<DurationConverter>();
            CreateMap<long?, Duration?>().ConvertUsing<DurationConverter>();
            CreateMap<Duration, int>().ConvertUsing<DurationConverter>();
            CreateMap<Duration?, int?>().ConvertUsing<DurationConverter>();
            CreateMap<int, Duration>().ConvertUsing<DurationConverter>();
            CreateMap<int?, Duration?>().ConvertUsing<DurationConverter>();
            CreateMap<Duration, double>().ConvertUsing<DurationConverter>();
            CreateMap<Duration?, double?>().ConvertUsing<DurationConverter>();
            CreateMap<double, Duration>().ConvertUsing<DurationConverter>();
            CreateMap<double?, Duration?>().ConvertUsing<DurationConverter>();
            CreateMap<Duration, decimal>().ConvertUsing<DurationConverter>();
            CreateMap<Duration?, decimal?>().ConvertUsing<DurationConverter>();
            CreateMap<decimal, Duration>().ConvertUsing<DurationConverter>();
            CreateMap<decimal?, Duration?>().ConvertUsing<DurationConverter>();
        }

        private void CreateMappingsForInstantConvertor()
        {
            CreateMap<Instant, DateTime>().ConvertUsing<InstantConverter>();
            CreateMap<Instant?, DateTime?>().ConvertUsing<InstantConverter>();
            CreateMap<Instant, DateTimeOffset>().ConvertUsing<InstantConverter>();
            CreateMap<Instant?, DateTimeOffset?>().ConvertUsing<InstantConverter>();
            CreateMap<DateTime, Instant>().ConvertUsing<InstantConverter>();
            CreateMap<DateTime?, Instant?>().ConvertUsing<InstantConverter>();
            CreateMap<DateTimeOffset, Instant>().ConvertUsing<InstantConverter>();
            CreateMap<DateTimeOffset?, Instant?>().ConvertUsing<InstantConverter>();
        }

        private void CreateMappingsForLocalDateConverter()
        {
            CreateMap<LocalDate, DateTime>().ConvertUsing<LocalDateConverter>();
            CreateMap<LocalDate?, DateTime?>().ConvertUsing<LocalDateConverter>();
            CreateMap<DateTime, LocalDate>().ConvertUsing<LocalDateConverter>();
            CreateMap<DateTime?, LocalDate?>().ConvertUsing<LocalDateConverter>();
        }

        private void CreateMappingsForLocalDateTimeConverter()
        {
            CreateMap<LocalDateTime, DateTime>().ConvertUsing<LocalDateTimeConverter>();
            CreateMap<LocalDateTime?, DateTime?>().ConvertUsing<LocalDateTimeConverter>();
            CreateMap<DateTime, LocalDateTime>().ConvertUsing<LocalDateTimeConverter>();
            CreateMap<DateTime?, LocalDateTime?>().ConvertUsing<LocalDateTimeConverter>();
        }

        private void CreateMappingsForLocalTimeConverter()
        {
            CreateMap<LocalTime, TimeSpan>().ConvertUsing<LocalTimeConverter>();
            CreateMap<LocalTime?, TimeSpan?>().ConvertUsing<LocalTimeConverter>();
            CreateMap<TimeSpan, LocalTime>().ConvertUsing<LocalTimeConverter>();
            CreateMap<TimeSpan?, LocalTime?>().ConvertUsing<LocalTimeConverter>();
            CreateMap<LocalTime, DateTime>().ConvertUsing<LocalTimeConverter>();
            CreateMap<LocalTime?, DateTime?>().ConvertUsing<LocalTimeConverter>();
            CreateMap<DateTime, LocalTime>().ConvertUsing<LocalTimeConverter>();
            CreateMap<DateTime?, LocalTime?>().ConvertUsing<LocalTimeConverter>();
        }

        private void CreateMappingsForOffsetConverter()
        {
            CreateMap<Offset, TimeSpan>().ConvertUsing<OffsetConverter>();
            CreateMap<Offset?, TimeSpan?>().ConvertUsing<OffsetConverter>();
            CreateMap<TimeSpan, Offset>().ConvertUsing<OffsetConverter>();
            CreateMap<TimeSpan?, Offset?>().ConvertUsing<OffsetConverter>();
        }

        private void CreateMappingsForOffsetDateTimeConverter()
        {
            CreateMap<OffsetDateTime, DateTimeOffset>().ConvertUsing<OffsetDateTimeConverter>();
            CreateMap<OffsetDateTime?, DateTimeOffset?>().ConvertUsing<OffsetDateTimeConverter>();
            CreateMap<DateTimeOffset, OffsetDateTime>().ConvertUsing<OffsetDateTimeConverter>();
            CreateMap<DateTimeOffset?, OffsetDateTime?>().ConvertUsing<OffsetDateTimeConverter>();
        }

        private void CreateMappingsForPeriodConverter()
        {
            CreateMap<Period, string>().ConvertUsing<PeriodConverter>();
            CreateMap<string, Period>().ConvertUsing<PeriodConverter>();
        }
    }
}