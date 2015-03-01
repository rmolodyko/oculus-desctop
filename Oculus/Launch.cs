using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oculus
{
    class Launch : Subject
    {
        Process process = new Process();
        Stopwatch stopWatch = new Stopwatch();
        TimeSpan duration = new TimeSpan();

        public Launch(String pathToProgram, String args = "")
        {
            process.StartInfo.FileName = pathToProgram;
            if (args != "")
            {
                process.StartInfo.Arguments = args;
            }
        }

        public void startProgram(String mode,int id_game)
        {
            try
            {
                publish("start");
                Console.WriteLine("--------");
                stopWatch.Start();
                process.Start();

                process.WaitForExit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (process != null)
                {

                    stopProgram(mode,id_game);
                }
            }
        }

        public void stopProgram(String mode, int id_game)
        {
            stopWatch.Stop();
            process.Close();
            duration = stopWatch.Elapsed;
            publish("stop");
            TextConfig textConfig = new TextConfig();
            if(mode != "promo")
            textConfig.writeSessionPlay(id_game,duration.Seconds);

        }

        public TimeSpan getDuration()
        {
            return duration;
        }

    }
}
