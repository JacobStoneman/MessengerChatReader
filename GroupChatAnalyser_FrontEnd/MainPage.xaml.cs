using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using GroupChatAnalyser_Utils;
using Windows.Storage;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GroupChatAnalyser_FrontEnd
{

    public sealed partial class MainPage : Page
    {
        StorageFolder logDir;

        Analyser ChatAnalyser;

        public MainPage()
        {
            InitializeComponent();
        }

		async void Init()
		{
			await ChatAnalyser.Init();

            txt_title.Text = ChatAnalyser.ChatName;
            txt_totalMessages.Text = ChatAnalyser.ChatLog.messages.Count.ToString() + " Total Messages";
            txt_dates.Text = "Between " + ChatAnalyser.StartDate + " And " + ChatAnalyser.EndDate;

            foreach(Participant member in ChatAnalyser.ChatLog.participants)
			{
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = member.name;
                listBox_members.Items.Add(newItem);
            }

            GenerateChart();
        }

		private async void btn_generate_Click(object sender, RoutedEventArgs e)
		{
			Windows.Storage.Pickers.FolderPicker picker = new Windows.Storage.Pickers.FolderPicker();

            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add("*");

            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;

			logDir = await picker.PickSingleFolderAsync();

            ChatAnalyser = new Analyser(logDir);

            Init();
        }

		private void listView_members_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            string selectedOption = ((ListBoxItem)((ListBox)sender).SelectedItem).Content.ToString();

            DisplayInfo(ChatAnalyser.GetParticipantByName(selectedOption));
		}

        private void DisplayInfo(Participant member)
		{
            txt_totalMessages_participants.Text = "Total messages sent: " + member.TotalMessagesSent.ToString();
        }

        private void GenerateChart()
		{
            List<PieChartEntry> data = new List<PieChartEntry>();

            foreach(Participant member in ChatAnalyser.ChatLog.participants)
			{
                data.Add(new PieChartEntry(member.name, member.TotalMessagesSent));
			}

            (PieChart.Series[0] as PieSeries).ItemsSource = data;
        }
	}
}
