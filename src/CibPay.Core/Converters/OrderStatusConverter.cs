using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CibPaySdk.Core.Types;

namespace CibPaySdk.Core.Converters;

public class OrderStatusConverter : JsonConverter<OrderStatuses?>
{
	public override OrderStatuses? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.GetString() switch
		{

			"new" => OrderStatuses.New,
			"prepared" => OrderStatuses.Prepared,
			"authorized" => OrderStatuses.Prepared,
			"charged" => OrderStatuses.Charged,
			"reversed" => OrderStatuses.Reversed,
			"refunded" => OrderStatuses.Refunded,
			"rejected" => OrderStatuses.Rejected,
			"fraud" => OrderStatuses.Fraud,
			"declined" => OrderStatuses.Declined,
			"chargedback" => OrderStatuses.ChargedBack,
			"credited" => OrderStatuses.Credited,
			"error" => OrderStatuses.Error,

			_ => null

		};
	}

	public override void Write(Utf8JsonWriter writer, OrderStatuses? value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}
}
