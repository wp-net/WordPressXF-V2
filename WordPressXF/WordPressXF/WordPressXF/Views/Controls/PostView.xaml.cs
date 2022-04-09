using System;
using WordPressPCL.Models;
using WordPressXF.Init;
using WordPressXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostView : ContentView
    {
        public static readonly BindableProperty EmbeddedProperty =
           BindableProperty.Create(nameof(Embedded), typeof(Embedded), typeof(PostView));

        public Embedded Embedded
        {
            get => (Embedded)GetValue(EmbeddedProperty);
            set => SetValue(EmbeddedProperty, value);
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(PostView));

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(PostView));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty ExcerptProperty =
            BindableProperty.Create(nameof(Excerpt), typeof(string), typeof(PostView));

        public string Excerpt
        {
            get => (string)GetValue(ExcerptProperty);
            set => SetValue(ExcerptProperty, value);
        }

        public PostView()
        {
            InitializeComponent();
        }

        private async void FrameOnTapped(object sender, EventArgs e)
        {
            // get View object from sender
            if (sender is not View viewSender)
                return;

            // get MeasurementItem from BindingContext
            if (viewSender.BindingContext is not Post post)
                return;

            // show animation
            var scale = viewSender.Scale;
            await viewSender.ScaleTo(scale * 0.95, 50);
            await viewSender.ScaleTo(scale, 50);

            // click logic
            await Bootstrapper.ServiceProvider.GetService<PostsViewModel>().SetSelectedPostCommand.ExecuteAsync(post);
        }
    }
}