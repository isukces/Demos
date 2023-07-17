namespace Demo01_AutoRegisterMediatorHandlers;

public static class Extensions
{
    public static string FriendlyName(this Type t)
    {
        if (!t.IsGenericType)
            return t.Name;
        
        var g = t.GetGenericArguments();
        return t.Name.Substring(0, t.Name.IndexOf('`')) + "<" +
               string.Join(",", g.Select(FriendlyName)) + ">";
    }
}
