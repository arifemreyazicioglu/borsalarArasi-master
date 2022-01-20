namespace BinanceTR.Core.Results.Abstract
{
    public interface IResult
    {
        long Code { get; }
        bool Success { get; }
        string Message { get; }
    }
}
