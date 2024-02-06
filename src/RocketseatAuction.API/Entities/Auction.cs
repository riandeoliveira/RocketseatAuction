using System.ComponentModel.DataAnnotations.Schema;

namespace RocketseatAuction.API.Entities;

[Table("Auctions")]
public class Auction
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime Starts { get; set; }

    public DateTime Ends { get; set; }

    public List<Item> Items { get; set; } = [];
}
