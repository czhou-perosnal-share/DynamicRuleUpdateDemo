namespace DynamicRuleUpdateDemo;

/// <summary>
/// This is a demo,
/// all the properties are considered as And condition.
/// all the property's values are considered as equal condition.
/// </summary>
public class DynamicRule
{
    public string Name { get; set; }
    public Dictionary<string, string> Conditions { get; set; }
}