#nullable enable

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ShareTechUWP.SubPages.Obsolete
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Obsolete]
    public sealed partial class TmiBoard : Page
    {
        private static class Ftp
        {
            public static string Path => ConfigurationManager.AppSettings["ConetFTP"];
            public static string User => ConfigurationManager.AppSettings["ConetFTPUser"];
            public static string Password => ConfigurationManager.AppSettings["ConetFTPPassword"];
        }


        private static string BoardTmpFile => ApplicationData.Current.LocalFolder.Path.Replace('/', '\\').TrimEnd('\\') + "\\tmis.xml";

        public static Frame? NavigationPageFrame { get; set; } = null;
        public static List<PostData> PostList { get; set; } = new();

        public TmiBoard()
        {
            InitializeComponent();
            DownloadData();
            LoadPosts();
        }

        private void LoadPosts()
        {
            if (PostList is null || PostList.Any() is false)
                return;

            int buttons = 0;
            foreach (var post in PostList)
            {
                PostsGrid.Children.Add(new PostButton(post, PostButton_Click, buttons++));
            }
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is PostButton pb)
            {
                PostPage.Post = pb.Post;
                Frame.Navigate(typeof(PostPage), null, new DrillInNavigationTransitionInfo());
            }
        }

        private void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PostPage), null, new DrillInNavigationTransitionInfo());
        }

        public static void UploadData()
        {
            // 1. PostData Serialization to XML
            XmlSerializer serializer = new(typeof(List<PostData>));
            using (TextWriter writer = new StreamWriter(BoardTmpFile))
            {
                serializer.Serialize(writer, PostList);
            }

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(Ftp.Path);
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential(Ftp.User, Ftp.Password);

            byte[] data = File.ReadAllBytes(BoardTmpFile);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
                reqStream.Write(data, 0, data.Length);

            using FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
        }

        private void DownloadData()
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(Ftp.Path);
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.Credentials = new NetworkCredential(Ftp.User, Ftp.Password);

            // FTP Request 결과를 가져온다.
            using FtpWebResponse resp = (FtpWebResponse)req.GetResponse();
            // FTP 결과 스트림
            using (Stream stream = resp.GetResponseStream())
            {
                string data;
                using (StreamReader reader = new(stream))
                    data = reader.ReadToEnd();

                File.WriteAllText(BoardTmpFile, data);
            }

            XmlSerializer serializer = new(typeof(List<PostData>));
            using FileStream fs = new(BoardTmpFile, FileMode.Open);
            PostList = (List<PostData>)serializer.Deserialize(fs);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadData();
            PostsGrid.Children.Clear();
            LoadPosts();
        }
    }
}