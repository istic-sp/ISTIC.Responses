namespace ISTIC.Responses.Core;

public class Error
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, List<string>> FieldErrors { get; set; }

    public Error(string name, string description, Dictionary<string, List<string>> fieldErrors = null)
    {
        Name = name;
        Description = description;
        FieldErrors = fieldErrors ?? new Dictionary<string, List<string>>();
    }

    public void AddFieldError(string fieldName, string errorMessage)
    {
        if (FieldErrors.ContainsKey(fieldName))
            FieldErrors[fieldName].Add(errorMessage);
        else
            FieldErrors[fieldName] = new List<string> { errorMessage };
    }

    public void AddFieldError(string fieldName, List<string> errorMessages)
    {
        if (FieldErrors.ContainsKey(fieldName))
            FieldErrors[fieldName].AddRange(errorMessages);
        else
            FieldErrors[fieldName] = new List<string>(errorMessages);
    }
}
