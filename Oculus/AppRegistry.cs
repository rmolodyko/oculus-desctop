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
    }
}
