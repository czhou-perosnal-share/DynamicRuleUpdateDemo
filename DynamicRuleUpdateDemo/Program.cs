// See https://aka.ms/new-console-template for more information
using DynamicRuleUpdateDemo;


var contentList = new []
{
   new Content
   {
      Name = "Chao Chinese", Description = "This is demo content", 
      CustomProperty = new()
      {
         {"Author", "Chao"},
         {"Language", "Chinses"}
      }
   },
   new Content
   {
      Name = "Alex Chinese", Description = "This is demo content", 
      CustomProperty = new()
      {
         {"Author", "Alex"},
         {"Language", "Chinses"}
      }
   },
   new Content
   {
      Name = "Chao English", Description = "This is another demo content", 
      CustomProperty = new()
      {
         {"Author", "Chao"},
         {"Language", "English"}
      }
   },
};


var ruleList = new[]{ 
   new DynamicRule
   {
      Name = "Language is English",
      Conditions = new()
      {
         {"Language", "English"}
      }
   },
   new DynamicRule
   {
      Name = "Language is Chinses",
      Conditions = new()
      {
         {"Language", "Chinses"}
      }
   }
};

var matchedResultDic = new Dictionary<DynamicRule, ICollection<Content>>();
matchedResultDic.Add(ruleList[0],DynamicRuleResultResolver.GetMatchedContents(ruleList[0], contentList));
matchedResultDic.Add(ruleList[1],DynamicRuleResultResolver.GetMatchedContents(ruleList[1], contentList));

// Update the content "Chao Chinese"'s Language property from Chinese to English 
// so we will remove content "Chao Chinese" from the matched contents of rule "Language is Chinses"
// and add it to the matched contents of rule "Language is English"
// because this is a demo, so the content changes info is hard coded here

//Apply the change and save the changes info
contentList[0].CustomProperty["Language"] = "English";
var changedPropertyName = "Language";
var originalValue = "Chinses";
var newValue = "English";

//Find previous matched rule from matchedResultDic
//Then to validate the content is still matched or not, if not removed from the matched contents
foreach (var kvp in matchedResultDic)
{
    var rule = kvp.Key;
    var matchedContents = kvp.Value;

    if (rule.Conditions.ContainsKey(changedPropertyName) && rule.Conditions[changedPropertyName] == originalValue)
    {
        if (matchedContents.Any(content => content.Name == contentList[0].Name))
        {
            if (DynamicRuleResultResolver.IsMatched(contentList[0],rule))
            {
                //The content is still matched
                continue;
            }
            else
            {
                //The content is not matched anymore
                matchedContents.Remove(contentList[0]);
            }
        }
    }
}

//Find the possbile new matched rule by the condition with the new value from matchedResultDic
//Then to validate the content is matched or not, if yes added it into the matched contents
foreach (var kvp in matchedResultDic)
{
    var rule = kvp.Key;
    var matchedContents = kvp.Value;

    if (rule.Conditions.ContainsKey(changedPropertyName) && rule.Conditions[changedPropertyName] == newValue)
    {
        if (DynamicRuleResultResolver.IsMatched(contentList[0],rule))
        {
            //The content is matched
            matchedContents.Add(contentList[0]);
        }
    }
}


