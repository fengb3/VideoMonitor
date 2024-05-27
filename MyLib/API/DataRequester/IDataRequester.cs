namespace MyLib.API.DataRequester;

public interface IDataRequester<T>
{
    // public HttpClient Client { get; }
    
    public string BaseUrl { get; }
    
    public HttpMethod Method { get; }
    
    public Task<T> RequestData(Dictionary<string, string> parameters);
}