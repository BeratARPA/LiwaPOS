namespace LiwaPOS.BLL.ValueChangeSystem
{
    public interface IDynamicValueResolver
    {
        string ResolveExpression(string expression, ValueContext context);
    }
}
