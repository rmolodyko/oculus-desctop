using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oculus
{
    class Web
    {
        static AppRegistry web = AppRegistry.getInstance();

        static public string GET(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url + "?" + Data);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }

        static public bool getEmployeeBind(int id_place)
        {
            try
            {
                String json = Web.GET(web.serverUri, "r=" + "operate/create/select&id_place=" + id_place);
                dynamic res = JsonConvert.DeserializeObject(json);
                web.employee_email = res.email;
                web.emlloyee_id_bind = res.id_bind;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static public void sendDataGame(int id_game,int duration,int ts_start) { 
            
        }

    }
}
