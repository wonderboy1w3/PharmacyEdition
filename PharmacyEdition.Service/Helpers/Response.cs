namespace PharmacyEdition.Service.Helpers;

public class Response<TResult>
{
    // Header
    public int StatusCode { get; set; }
    public string Message { get; set; }

    // Body
    public TResult Value { get; set; }
}
