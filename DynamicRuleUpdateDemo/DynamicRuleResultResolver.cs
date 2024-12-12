namespace DynamicRuleUpdateDemo;

public class DynamicRuleResultResolver
{
    public static ICollection<Content> GetMatchedContents(DynamicRule rule, ICollection<Content> contents)
    {
        return contents.Where(content => IsMatched(content, rule)).ToList();
    }

    public static bool IsMatched(Content content, DynamicRule rule)
    {
        foreach (var condition in rule.Conditions)
        {
            if (condition.Key == "Name")
            {
                if (content.Name != condition.Value)
                {
                    return false;
                }
            }
            else if (condition.Key == "Description")
            {
                if (content.Description != condition.Value)
                {
                    return false;
                }
            }
            else
            {
                if (!content.CustomProperty.ContainsKey(condition.Key) 
                    || content.CustomProperty[condition.Key] != condition.Value)
                {
                    return false;
                }
            }
        }

        return true;
    }
}