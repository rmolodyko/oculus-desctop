using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oculus
{
    public class AppRegistry
    {
        static public AppRegistry instance;

        public int id_place;
        public int id_bind;
        public int last_active;
        public List<String> employee_email = new List<String>();
        public List<int> employee_id_bind = new List<int>();
        public String passwordSetting = "123";

        public String promoPath;
        public String promoArgs;

        public String[] game_path = new String[30];
        public String[] game_args = new String[30];
        public int[] game_id = new int[30];

        public String[] thumb_path = new String[30];

        public String serverUri;
        public String pathToConfigPromo = @"config\promo.txt";
        public String pathToConfigPlace = @"config\place.txt";
        public String pathToConfigGames = @"config\games.txt";
        public String pathToConfigMain = @"config\main.txt";
        public String pathToConfigThumb = @"config\thumb.txt";

        public String pathToEmailEmployee = @"config\dynamic\employee.txt";
        public String pathToSessionDirectory = @"config\dynamic\session_employee\";
        public String pathToSessionPlayDirectory = @"config\dynamic\session_play\";

        public int widthMainGrid;
        public int heightMainGrid;
        public int marginMainGrid = 10;

        private AppRegistry(){}
        static public AppRegistry getInstance(){
            if (instance == null) {
                instance = new AppRegistry();
            }
            return instance;
        }

        public String getPathToSessionPlay()
        {
            String path = pathToSessionPlayDirectory;
            bool exists = System.IO.Directory.Exists(path+id_bind);
            if (!exists)
                System.IO.Directory.CreateDirectory(path+id_bind);

            return path+id_bind+"\\"+tsToday()+".txt";
        }

        public String getPathToSessionEmployee()
        {
            String path = pathToSessionDirectory;
            bool exists = System.IO.Directory.Exists(path+id_bind);
            if (!exists)
                System.IO.Directory.CreateDirectory(path+id_bind);

            return path+id_bind+"\\"+tsToday()+".txt";
        }

        public Int32 tsToday()
        {
            int day = DateTime.UtcNow.Day; 
            int month = DateTime.UtcNow.Month; 
            int year = DateTime.UtcNow.Year; 
            Int32 tsu = (Int32)((new DateTime(year,month,day)).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return tsu+86399;
        }
    }
}
