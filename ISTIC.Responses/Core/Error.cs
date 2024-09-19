namespace ISTIC.Responses.Core;

public class Error
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DictionaryError FieldErrors { get; set; }

    public Error(string name, string description, Dictionary<string, List<string>> fieldErrors = null)
    {
        Name = name;
        Description = description;
        FieldErrors = new DictionaryError(fieldErrors);
    }
}

public class DictionaryError : Dictionary<string, List<string>>
{
    public DictionaryError(Dictionary<string, List<string>> dictionary = null)
    {
        if (dictionary != null)
            foreach (var item in dictionary)
                Add(item.Key, item.Value);
    }

    public void Add(string fieldName, string errorMessage)
    {
        if (ContainsKey(fieldName))
            this[fieldName].Add(errorMessage);
        else
            this[fieldName] = new List<string> { errorMessage };
    }

    public void Add(string fieldName, List<string> errorMessage)
    {
        if (ContainsKey(fieldName))
            this[fieldName].AddRange(errorMessage);
        else
            this[fieldName] = new List<string>(errorMessage);
    }

    public void Add(Dictionary<string, List<string>> fieldErrors)
    {
        foreach (var item in fieldErrors)
            Add(item.Key, item.Value);
    }
}