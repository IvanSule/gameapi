using RPSSL.Domain.Enums;
using System.Text.Json.Serialization;

namespace RPSSL.Application.Play
{
    public sealed record PlayResponse(string Results, RPSSLOptions Player, RPSSLOptions Computer, [property: JsonIgnore] Guid ScoreId);
}
