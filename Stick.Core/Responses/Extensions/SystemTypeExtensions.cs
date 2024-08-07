namespace Stick.Core.Responses.Extensions
{
    public static class SystemTypeExtensions
    {
        public static string GetSchemaId(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericArguments = string.Join(",", type.GenericTypeArguments.Select(t => t.GetSchemaId()));
                return $"{type.Name[..type.Name.IndexOf('`')]}[{genericArguments}]";
            }

            return type.FullName.Replace('+', '.');
        }
    }
}
