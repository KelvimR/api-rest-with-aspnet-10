namespace api_rest_with_aspnet_10.Data.Converter.Contract;

public interface IParser<O, D>
{
    D Parse(O origin);
    List<D> ParseList(List<O> origin);
}
