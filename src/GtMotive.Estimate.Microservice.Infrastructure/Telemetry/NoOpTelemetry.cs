using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Telemetry
{
    [ExcludeFromCodeCoverage]
    public class NoOpTelemetry : ITelemetry
    {
        public NoOpTelemetry()
        {
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            Console.WriteLine($"[TELEMETRY EVENT]: {eventName}");
            if (properties != null)
            {
                foreach (var p in properties)
                {
                    Console.WriteLine($"  Prop: {p.Key} = {p.Value}");
                }
            }
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            Console.WriteLine($"[TELEMETRY METRIC]: {name} = {value}");
        }
    }
}
