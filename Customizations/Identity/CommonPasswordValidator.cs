using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AdminArea_IdentityBase.Customizations.Identity
{
    public class CommonPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        private readonly string[] commons;
        public CommonPasswordValidator()
        {
            //Elenco di password comuni
            //Un elenco più significativo, lo si può ottenere da:
            //https://github.com/danielmiessler/SecLists/tree/master/Passwords/Common-Credentials
            
            this.commons = new [] {
                //"password", "abc", "123", "qwerty", "123456",
                "password","12345678","qwerty","12345","123456789","letmein","1234567","football",
                "iloveyou","admin","welcome","monkey","login","abc123","starwars","123123","dragon",
                "passw0rd","master","hello","freedom","whatever","qazwsx","trustno1","654321","jordan23",
                "harley","password01","1234","robert","matthew","jordan","asshole","daniel","andrew",
                "lakers","andrea","buster","johsua","1qaz2wsx","12341234","ferrari","cheese","computer",
                "corvette","blahblah","george","mercedes","121212","maverick","fuckyou","nicole","hunter",
                "sunshine","tigger","1989","merlin","ranger","solo","banana","chelsea","summer","1990","1991",
                "phoenix","amanda","cookie","ashley","bandit","killer","aaaaa","pepper","jessica","zaq1zaq1",
                "jennifer","test","hockey","dallas","passwor","michelle","admin123","pussy","pass","asdf",
                "william","soccer","london","1q2w3e","!1992","!biteme","maggie","querty","rangers","charlie",
                "martin","ginger","golfer","yankees","thunder"
            };
        }

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            IdentityResult result;
            if (commons.Any(common => password.Contains(common, StringComparison.CurrentCultureIgnoreCase)))
            {
                result = IdentityResult.Failed(new IdentityError { Description = "Password troppo comune" });
            }
            else
            {
                result = IdentityResult.Success;
            }
            return Task.FromResult(result);
        }
    }
}