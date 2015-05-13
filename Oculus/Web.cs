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
            Console.WriteLine(Url + "?" + Data);
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
                String json = Web.GET(web.serverUri, "r=" + "remote/data/loginEmployee&id_place=" + id_place);
                dynamic res = convertJsonToDinamic(json);
                Console.WriteLine(res.email[0]);
                foreach (var i in res.email)
                {
                    web.employee_email.Add((String)i);
                }
                foreach (var i in res.id_bind)
                {
                    web.employee_id_bind.Add((int)i);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new WebException("error");
                //return false;
            }
        }

        static public void sendDataSessionEmployee(List<String> json, String type)
        {
            try
            {
                Console.WriteLine(serialazeObject(json));
                String json_res = Web.GET(web.serverUri, "r=" + "remote/data/setSession&id_bind=" + web.id_bind+"&json="+serialazeObject(json)+"&type="+type);
                Console.WriteLine(json_res);
                //if(type == "employee")
                //web.last_active = Int32.Parse(json_res);
            }catch(Exception ex){

            }
        }
        static public void getLastActive()
        {
            String json = Web.GET(web.serverUri, "r=" + "remote/data/getLastActive&id_bind=" + web.id_bind);
            web.last_active = Int32.Parse(json);
        }

        static public dynamic convertJsonToDinamic(String json)
        {
            dynamic res = JsonConvert.DeserializeObject(json);
            return res;
        }

        static public String serialazeObject(dynamic o)
        {
            String res = JsonConvert.SerializeObject(o);
            return res;
        }

        static public void sendDataGame(int id_game,int duration,int ts_start) { 
            
        }
    }
}
