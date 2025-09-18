using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Grocery.Core.Data")]


namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        public string _emailAddress;
        protected internal string _password { get; set; }
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            _emailAddress=emailAddress;
            _password=password;
        }
    }
}
