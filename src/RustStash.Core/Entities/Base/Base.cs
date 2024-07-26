using HotChocolate.Types.Relay;

namespace RustStash.Core.Entities.Base;

public class Base
{
    [ID]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }
}