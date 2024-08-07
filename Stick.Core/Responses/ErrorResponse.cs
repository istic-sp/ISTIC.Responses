namespace Stick.Core.Responses;

public class ErrorResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string[]> FieldErrors { get; set; }

    public ErrorResponse(string name, string description, Dictionary<string, string[]> fieldErrors = null)
    {
        Name = name;
        Description = description;
        FieldErrors = fieldErrors ?? new Dictionary<string, string[]>();
    }
}
