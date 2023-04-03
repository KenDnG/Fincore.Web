namespace FINCORE.Services.Helpers.Response.Interface
{
    public interface IObjectConverter
    {
        IList<TModel> JsonToList<TModel>(object content);

        T JsonToObject<T>(object content);

        //T JsonToObjectModel<T>(object content);
    }
}