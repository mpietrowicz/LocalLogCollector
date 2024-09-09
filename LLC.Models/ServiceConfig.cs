namespace LLC.Models;

public class ServiceConfig 
{
    public string Id { get; set; }
    private IDictionary<string,string> Keys { get; set; } = new Dictionary<string, string>();
    
    public string this[string key]
    {
        get => Keys[key];
        set => Keys[key] = value;
    }
    
    public void Add(string key, string value)
    {
        Keys.Add(key, value);
    }
    
    public void Remove(string key)
    {
        Keys.Remove(key);
    }
    
    public bool ContainsKey(string key)
    {
        return Keys.ContainsKey(key);
    }
    
    public bool TryGetValue(string key, out string value)
    {
        return Keys.TryGetValue(key, out value);
    }
    
    public void Clear()
    {
        Keys.Clear();
    }
    
    public IEnumerable<string> KeysCollection => Keys.Keys;
    
    public IEnumerable<string> Values => Keys.Values;
    
    public int Count => Keys.Count;
    
    public bool IsReadOnly => Keys.IsReadOnly;
    
}