using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Diagnostics;

namespace Oculus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppRegistry web = AppRegistry.getInstance();
        public TextConfig textConfig = new TextConfig();

        public MainWindow()
        {
            InitializeComponent();
            textConfig.init();
            initWindow();
            initialFillEmployeeComboBox();

        }

        private void initWindow() {
            if (web.widthMainGrid > 0)
                gridElements.Width = web.widthMainGrid;

            if (web.heightMainGrid > 0)
                gridElements.Height = web.heightMainGrid;
        }

        private void initialFillEmployeeComboBox() {

            if (File.Exists(web.pathToEmailEmployee))
            {
                textConfig.getEmailEmployee();
                Console.WriteLine("1-----");
            }
            else
            {
                Web.getEmployeeBind(web.id_place);
                textConfig.setEmailEmployee();
                Console.WriteLine("2-----");
            }

            if(web.employee_email != null)
            foreach (String i in web.employee_email)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stackPanelLogin.Visibility = System.Windows.Visibility.Hidden;
            stackPanelMain.Visibility = System.Windows.Visibility.Visible;

            int count = 1;
            for (int i = 0; i <= 2; i++ )
            {
                for (int j = 0; j <= 2; j++ )
                {
                    addStackPanel(gridElements, i, j,count);
                    count++;
                }
            }

            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    addStackPanel(gridElements1, i, j, count);
                    gridElements1.Margin = new Thickness(
                                                gridElements1.Margin.Right,    
                                                gridElements1.Margin.Top,
                                                gridElements1.Margin.Left,
                                                0
                        );
                    count++;
                }
            }
        }

        private void Click_Promo(object sender, RoutedEventArgs e)
        {
            Launch l = new Launch(web.promoPath,web.promoArgs);
            l.startProgram();
            Console.WriteLine(l.getDuration());

        }

        private void Click_ShotDown(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выключить ПК?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            var dialog = new WindowExit();
            if (dialog.ShowDialog() == true)
            {
                if (dialog.ResponseText == web.passwordSetting)
                    Application.Current.Shutdown();
            }
        }

        private void addStackPanel(Grid stackPanel,int row, int column,int count){

            VideoDrawing drawing = new VideoDrawing();
            drawing.Rect = new Rect(0, 0, 400, 300);
            drawing.Player = getMediaPlayer(count - 1);
            DrawingBrush brush = new DrawingBrush(drawing);
            Label sp = new Label();
            Console.WriteLine("margin " + web.marginMainGrid.ToString());
            sp.Margin = new Thickness(web.marginMainGrid);
            sp.Background = brush;
            sp.Opacity = 100;
            sp.Uid = count.ToString();
            sp.MouseDown += clickGame;
            sp.BorderThickness = new Thickness(1);
            stackPanel.Children.Add(sp);
            Grid.SetColumn(sp, column);
            Grid.SetRow(sp, row);

        }

        private MediaPlayer getMediaPlayer(int count) {
            MediaPlayer mp = new MediaPlayer();
            try
            {
                mp.Open(new Uri(web.thumb_path[count]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            mp.Volume = 0;
            mp.MediaEnded += new EventHandler(Media_Ended);
            mp.Play();

            return mp;
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            ((MediaPlayer)sender).Position = TimeSpan.Zero;
            ((MediaPlayer)sender).Play();
        }
        private void clickGame(object sender, RoutedEventArgs e)
        {
            Label btn = ((Label)sender);
            Console.WriteLine(btn.Uid);
            int num = int.Parse(btn.Uid);
            Launch l = new Launch(web.game_path[num], web.game_args[num]);
            l.startProgram();
            Console.WriteLine(l.getDuration());
        }

        private double heightPanel;
        private void Click_PreviousPanel(object sender, RoutedEventArgs e)
        {
            gridElements1.Visibility = System.Windows.Visibility.Hidden;
            gridElements.Visibility = System.Windows.Visibility.Visible;
            gridElements.Height = heightPanel;
            gridElements.Margin = new Thickness(50);
        }

        private void Click_NextPanel(object sender, RoutedEventArgs e)
        {
            heightPanel = gridElements.Height;
            gridElements.Visibility = System.Windows.Visibility.Hidden;
            gridElements1.Visibility = System.Windows.Visibility.Visible;
            gridElements.Height = 0;
            gridElements.Margin = new Thickness(0);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
