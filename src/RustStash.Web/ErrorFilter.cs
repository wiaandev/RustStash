namespace RustStash.Web;

using HotChocolate;

public class ErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        // TODO: Perform any custom error transforms
        return error;
    }
}
