using System;

namespace DAL
{
	public class UserEntity 
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public byte StatusId { get; set; }
		public Guid? Token { get; set; }

		public DateTime TokenExp { get; set; }

	}
}