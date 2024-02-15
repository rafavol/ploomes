using System.Runtime.Serialization;

namespace Ploomes.API.Models
{
    [DataContract]
    public class BaseModel
    {
        public long Id { get; set; }
    }
}
