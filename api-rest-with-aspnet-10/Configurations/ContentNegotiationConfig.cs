using Microsoft.Net.Http.Headers;

namespace api_rest_with_aspnet_10.Configurations;

public static class ContentNegotiationConfig
{
    //Aqui estamos configurando o Content Negociation para respeitar o header "Accept" do navegador, e retornar um status code 406 (Not Acceptable) caso o formato solicitado não seja suportado.
    //Além disso, estamos mapeando a extensão "xml" para o tipo de mídia "application/xml", estamos dizendo que aceitamos receber dados em formato XML, e caso o cliente envie uma requisição com a extensão "xml", o servidor irá responder com o tipo de mídia "application/xml".
    public static IMvcBuilder AddContentNegociation(this IMvcBuilder builder)
    {
        return builder.AddMvcOptions(options =>
        {
            options.RespectBrowserAcceptHeader = true;
            options.ReturnHttpNotAcceptable = true;

            //Informando que receberemos dados em formato XML, e caso o cliente envie uma requisição com a extensão "xml", o servidor irá responder com o tipo de mídia "application/xml".
            //Informando que receberemos dados em formato JSON, e caso o cliente envie uma requisição com a extensão "json", o servidor irá responder com o tipo de mídia "application/json".
            options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
            options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
        }).AddXmlSerializerFormatters();
    }
}
