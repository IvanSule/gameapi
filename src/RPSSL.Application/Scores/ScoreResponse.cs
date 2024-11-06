namespace RPSSL.Application.Scores
{
    public sealed record ScoreResponse(string PlayerOne, string PlayerTwo, string Result, DateTime Time)
    {
    }
}
