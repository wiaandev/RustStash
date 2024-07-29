using HotChocolate.Types.Relay;

namespace RustStash.Core.Entities.Inventory;

public class Inventory
{
    [ID]
    public int Id { get; set; }

    public string ItemName { get; set; }

    public string ItemImage { get; set; }

    public int Quantity { get; set; }

    [ID]
    public string UserId { get; set; }
}