#nullable enable

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
using RollingRess;
using static RollingRess.StaticClass;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ShareTechUWP.SubPages.Obsolete
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Obsolete]
    public sealed partial class PostPage : Page
    {
        public static PostData? Post { get; set; } = null;
        public PostPage()
        {
            InitializeComponent();
            CancelButton.Visibility = Visibility.Visible;
            NavigationPage.DeferralMode = NavigationDeferralType.WritingPost;
            if (Post is not null)
            {
                TitleTextBox.Text = Post.Title;
                BodyTextBox.Text = Post.Body;
                NickNameBox.Text = $"from. {Post.User.NickName}";
                NickNameBox.Visibility = Visibility.Visible;
                if (Post.User.Id == LoginPage.CurrentUser.Id)
                {
                    SaveButton.Visibility = Visibility.Visible;
                    DeleteButton.Visibility = Visibility.Visible;
                }

                LikeButton.Visibility = LikesCount.Visibility
                    = DislikeButton.Visibility = DislikesCount.Visibility = Visibility.Visible;

                LikesCount.Text = Post.Likes.Likes.ToString();
                DislikesCount.Text = Post.Likes.Dislikes.ToString();
            }
            else
            {
                SaveButton.Visibility = Visibility.Visible;
            }
            // 유저 아이디 확인. 동일하면 삭제 버튼 표시

            //  좋아요 및 싫어요
        }

        // 삭제 기능도 만들어야 함..

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreNullOrEmpty(TitleTextBox.Text, BodyTextBox.Text))
            {
                ContentDialog content = new()
                {
                    Title = "오류",
                    Content = "제목과 본문은 비워둘 수 없습니다.",
                    CloseButtonText = "확인",
                };
                await content.ShowAsync();
                return;
            }

            PostData post = new(LoginPage.CurrentUser, DateTime.Now, TitleTextBox.Text, BodyTextBox.Text, new());
            if (Post is not null)
            {
                int idx = TmiBoard.PostList.FindIndex(x => x == Post);
                TmiBoard.PostList[idx] = post;
            }
            else
            {
                TmiBoard.PostList.Add(post);
            }
            TmiBoard.PostList.Sort((x, y) => x.WrittenDate.CompareTo(y.WrittenDate));
            TmiBoard.UploadData();
            Close();
        }

        private void Close()
        {
            Post = null;
            Frame.Navigate(typeof(TmiBoard));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            // Post is not null
            Post!.Likes.DoLike();
            LikesCount.Text = Post.Likes.Likes.ToString();
            TmiBoard.PostList[TmiBoard.PostList.FindIndex(x => x == Post)] = Post;
        }

        private void DislikeButton_Click(object sender, RoutedEventArgs e)
        {
            // Post is not null
            int idx = TmiBoard.PostList.FindIndex(x => x == Post);

            Post!.Likes.DoDislike();
            DislikesCount.Text = Post.Likes.Dislikes.ToString();
            TmiBoard.PostList[idx] = Post;
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Modified)
            {
                ContentDialog content = new()
                {
                    Title = "삭제할 수 없음",
                    Content = "게시물의 내용이 변경되었습니다. 대신 저장이나 취소를 해보세요.",
                    CloseButtonText = "OK"
                };
                await content.ShowAsync();
                return;
            }
            
            ContentDialog contentDialog = new()
            {
                Title = "삭제",
                Content = $"정말로 이 글을 삭제할까요?",
                PrimaryButtonText = "예",
                CloseButtonText = "아니요"
            };
            if (await contentDialog.ShowAsync() is not ContentDialogResult.Primary)
                return;

            TmiBoard.UploadData();
            TmiBoard.PostList.Remove(Post!);
            contentDialog = new()
            {
                Title = "삭제 완료됨",
                Content = "삭제되었습니다.",
                CloseButtonText = "확인",
            };
            await contentDialog.ShowAsync();
            Close();
        }

        private bool Modified => Post is null ? false : Post.Title != TitleTextBox.Text || Post.Body != BodyTextBox.Text;
    }
}
