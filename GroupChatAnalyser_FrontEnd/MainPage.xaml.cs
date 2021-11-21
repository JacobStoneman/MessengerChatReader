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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GroupChatAnalyser_FrontEnd
{

    public sealed partial class MainPage : Page
    {
        StorageFolder logDir;
        StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

        Analyser ChatAnalyser;

        public MainPage()
        {
            this.InitializeComponent();

            //Init();
        }

		async void Init()
		{
			await ChatAnalyser.Init();

            txt_title.Text = ChatAnalyser.ChatName;

            foreach(Participant member in ChatAnalyser.ChatLog.participants)
			{
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = member.name;
                listBox_members.Items.Add(newItem);
            }
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
            ListBox box = ((ListBox)sender);
            ListBoxItem selected = (ListBoxItem)box.SelectedItem;
            txt_totalMessages.Text = ChatAnalyser.GetParticipantByName(selected.Content.ToString()).TotalMessagesSent.ToString();
		}
	}
}
