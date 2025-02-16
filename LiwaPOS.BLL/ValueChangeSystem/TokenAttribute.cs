namespace LiwaPOS.BLL.ValueChangeSystem
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TokenAttribute : Attribute
    {
        public string[] Tokens { get; }

        public TokenAttribute(params string[] tokens)
        {
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
        }
    }
}
