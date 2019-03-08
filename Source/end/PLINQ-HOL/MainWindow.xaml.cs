using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace HOL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<StatusItem> items = new ObservableCollection<StatusItem>();

        BackgroundWorker w;
        
        public MainWindow()
        {
            InitializeComponent();
            listBoxStatus.ItemsSource = items;

            w = new BackgroundWorker();
            w.DoWork += new DoWorkEventHandler(w_DoWork);
            w.WorkerReportsProgress = true;
            w.ProgressChanged += new ProgressChangedEventHandler(w_ProgressChanged);
            w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
        }

        void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonStart.IsEnabled = true;
            buttonStop.IsEnabled = false;
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            buttonStart.IsEnabled = false;
            buttonStop.IsEnabled = true;

            items.Clear();
            w.RunWorkerAsync();
        }

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Stopwatch timer = new Stopwatch();
            List<int> primes = new List<int>();

            try
            {
                // Insert code after this point
                ReportProgress("Ex3.Task1", -1, timer);
                Ex3.Task1();
            }
            catch (Exception x)
            {
                MessageBox.Show("An exception was caught: " + x.Message);
            }
        }

        void ReportProgress(string name, int qty, Stopwatch timer)
        {
            StatusItem item = null;
            lock (items)
            {
                // Update existing status item with time or create new entry
                item = (from si in items where si.Name == name select si).FirstOrDefault();
                if (item == null)
                    item = new StatusItem(name, qty, timer.ElapsedMilliseconds);
                else
                {
                    item.ElapsedTime = timer.ElapsedMilliseconds;
                    item.QtyFound = qty;
                }
            }
            w.ReportProgress(0, item);
        }

        void w_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lock (items)
            {
                if (!items.Contains(e.UserState as StatusItem))
                    items.Add(e.UserState as StatusItem);
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            Ex2.cts.Cancel();
            Ex2.cts = new CancellationTokenSource();
        }
    }
}
