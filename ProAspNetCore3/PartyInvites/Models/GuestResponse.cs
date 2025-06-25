using System.ComponentModel.DataAnnotations; 

namespace PartyInvites.Models 
{ 
	public class GuestResponse 
	{ 
		[Required(ErrorMessage = "Please enter your name")] 
		public string Name { get; set; } 
		
		[Required(ErrorMessage = "Please enter your email address")] 		
		[EmailAddress] 
		public string Email { get; set; } 
		
		[Required(ErrorMessage = "Please enter your phone number")] 
        [Phone]
        //[MaxLength(13)] //custom maximum length enabled
        //[MinLength(10)]//custom minimum length enabled
        [RegularExpression("^(\\+[1-8](?:[0-9] ?){5,13}[0-9]|[2-9][0-9]{9}|\\+[1-8][0-9]{0,2} ?\\([2-9][0-9]{2}\\) ?[0-9]{3}(-| )?[0-9]{4})$", ErrorMessage = "mobile number must be numeric")]
		public string Phone { get; set; } 
		
		[Required(ErrorMessage = "Please specify whether you'll attend")] 
		public bool? WillAttend { get; set; } 
	} 
}