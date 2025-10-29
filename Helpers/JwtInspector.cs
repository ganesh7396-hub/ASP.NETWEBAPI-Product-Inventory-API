using System.IdentityModel.Tokens.Jwt;

namespace Product_Inventory_Management_API.Helpers
{
    public static class JwtInspector
    {
        public static void InspectToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                Console.WriteLine("JWT Claims:");
                foreach (var claim in jwtToken.Claims)
                {
                    Console.WriteLine($" - {claim.Type}: {claim.Value}");
                }
            }
            else
            {
                Console.WriteLine("JWT is not well formed.");
            }
        }
    }
}
