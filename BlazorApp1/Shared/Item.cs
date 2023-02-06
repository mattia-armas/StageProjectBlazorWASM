using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Text.Json.Nodes;

namespace BlazorApp1.Shared;

public class Item
{
    public Item() { }
    public Item(Item parent)
    {
        ItemId = parent.Id;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string ItemId { get; set; }
    public bool HasSubFolder { get; set; }
    public bool Expanded { get; set; }
    public List<Item> Children { get; set; }
}
