using Newtonsoft.Json.Linq;

namespace Common.Domain.Interfaces
{
    public interface IDataLayerService
    {
        public void Add(JToken obj);
        JToken FindOne(string id);
        JToken FindMany();
    }
}