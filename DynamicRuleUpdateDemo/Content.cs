namespace DynamicRuleUpdateDemo;

public record Content
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string,string> CustomProperty { get; set; }
}