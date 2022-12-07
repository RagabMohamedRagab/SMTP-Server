using SendEmail.ViewsModel;
using System.Threading.Tasks;

namespace SendEmail.Utitles
{
    public interface ISendToEmail {
       Task<bool> SendMessage(FromToVM model);
       
    }
}
