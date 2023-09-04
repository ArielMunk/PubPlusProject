using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_full_name")]
        public string UserFullName { 
        get
            {
                return FirstName + " " + LastName;
            }
        }
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("user_name")]
        public string UserName {get;set;}

        [JsonProperty("status_id")]
        public int? StatusId { get; set; }

        [JsonProperty("status_name")]
        public string StatusName { get; set; }
    }
}
