using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oculus
{
    public class TextConfig
    {
        AppRegistry web = AppRegistry.getInstance();

        public void init() {
            initPlace();
            initPromo();
            initGames();
            initMain();
            initThumb();
        }

        public void setEmailEmployee() {
            List<String> json = new List<String>();
            if(web.employee_email != null)
            for (int i = 0; i < web.employee_email.Count; i++ )
            {
                dynamic res = new { email = web.employee_email[i], id_bind = web.employee_id_bind[i] };
                json.Add(Web.serialazeObject(res));
                System.IO.File.Delete(web.pathToEmailEmployee);
                System.IO.File.WriteAllLines(web.pathToEmailEmployee, json);
            }
        }

        public void getEmailEmployee()
        {
            List<String> ls = readFile(web.pathToEmailEmployee);
            foreach(String i in ls){
                if (i == "") continue;
                dynamic res = JsonConvert.DeserializeObject(i);
                web.employee_email.Add((String)res.email);
                web.employee_id_bind.Add((int)res.id_bind);
            }
        }

        public void initPromo(){
            List<string> ls = readFile(web.pathToConfigPromo);
            web.promoPath = ls[0];
            web.promoArgs = ls[1];
        }

        public void initPlace()
        {
            List<string> ls = readFile(web.pathToConfigPlace);
            web.id_place = int.Parse(ls[0]);
        }

        public void initGames()
        {
            List<string> ls = readFile(web.pathToConfigGames);
            int i = 0;
            int j = 0;
            foreach (String s in ls)
            {
                if (s == "")
                {
                    j = 0;
                    i++;
                    continue;
                }
                if (j == 0)
                {
                    web.game_path[i] = s;
                }
                if (j == 1)
                {
                    web.game_args[i] = s;
                }
                if (j == 2)
                {
                    web.game_id[i] = int.Parse(s);
                }
                j++;
            }
        }


        public void initThumb()
        {
            List<string> ls = readFile(web.pathToConfigThumb);
            int i = 0;
            foreach (String s in ls)
            {
                web.thumb_path[i++] = s;
            }
        }

        public void initMain() {
            List<string> ls = readFile(web.pathToConfigMain);
            web.serverUri = ls[0];
            if ((ls.Count > 1)&&(ls[1] != ""))
                web.widthMainGrid = int.Parse(ls[1]);
            if ((ls.Count > 2) && (ls[2] != ""))
                web.heightMainGrid = int.Parse(ls[2]);
            if ((ls.Count > 3) && (ls[3] != ""))
                web.marginMainGrid = int.Parse(ls[3]);
        }

        private List<string> readFile(String path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string str;
                List<string> ls = new List<string>();
                while ((str = sr.ReadLine()) != null)
                {
                    ls.Add(str);
                }
                sr.Close();
                fs.Close();
                return ls;

            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return new List<string>();

            }
        }

        public void writeSessionPlay(int id_game,int duration)
        {
            Console.WriteLine("play");
            List<String> json = new List<String>();
            Int32 tsu = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            dynamic res = new {ts = tsu, id_game = id_game, duration = duration};
            json.Add(Web.serialazeObject(res));
            System.IO.File.AppendAllLines(web.getPathToSessionPlay(), json);
        }

        public void writeSessionEmployee(String type)
        {
            List<String> json = new List<String>();
            Int32 tsu = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            dynamic res = new {ts = tsu, action = type};
            json.Add(Web.serialazeObject(res));
            System.IO.File.AppendAllLines(web.getPathToSessionEmployee(), json);
        }

        public List<String> readSessionPlay(String filename)
        {
            List<string> ls = readFile(web.pathToSessionPlayDirectory+web.id_bind+"\\"+filename+".txt");
            return ls;
        }

        public List<String> readSessionEmployee(String filename)
        {
            List<string> ls = readFile(web.pathToSessionDirectory+web.id_bind+"\\"+filename+".txt");
            return ls;
        }

        public List<String> getNameFiles(String path){
            var dir=new DirectoryInfo(path);
            var files = new List<string>();
            foreach (FileInfo file in dir.GetFiles())
            { 
                files.Add(Path.GetFileNameWithoutExtension(file.FullName));
            }
            return files;
        }

    }
}
