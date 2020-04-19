using Microsoft.AspNetCore.Identity;

namespace AppointmentSystem.Core.Entities
{
	// User is a persistent entity so it should be in Core layer despite the dependency on Identity framework.
	// Reference: https://stackoverflow.com/questions/53663652/referencing-applicationuser-in-the-infrastructure-library-from-an-entity-in-the
	public class ApplicationUser : IdentityUser
	{

	}
}
