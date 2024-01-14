using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareTechUWP
{
    public class Recording
    {
        public string CategoryName { get; set; }
        public string ImageSource { get; set; }
    }

    // 여기서 MainPage 그거 결정하는 건가
    public class RecordingViewModel
    {
        public ObservableCollection<Recording> Recordings { get; set; } = new();
        public RecordingViewModel()
        {
            Recordings.Add(new() { CategoryName = CategoryName.Deliver, ImageSource="다운로드.jpg" });
            Recordings.Add(new() { CategoryName = CategoryName.GameParty, ImageSource = "다운로드.jpg" });
            Recordings.Add(new() { CategoryName = CategoryName.Ott, ImageSource = "다운로드.jpg" });
            Recordings.Add(new() { CategoryName = CategoryName.TripMate, ImageSource = "Images/EVIDENCE.png" });
            Recordings.Add(new() { CategoryName = CategoryName.Food, ImageSource = "다운로드.jpg" });
            Recordings.Add(new() { CategoryName = CategoryName.Goods, ImageSource = "다운로드.jpg" });
        }
    }
}