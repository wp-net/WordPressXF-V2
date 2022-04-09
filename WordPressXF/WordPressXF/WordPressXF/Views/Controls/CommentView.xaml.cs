using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordPressXF.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentView : ContentView
    {
        public static readonly BindableProperty CommentProperty =
            BindableProperty.Create(nameof(Comment), typeof(string), typeof(CommentView));

        public string Comment
        {
            get => (string)GetValue(CommentProperty);
            set => SetValue(CommentProperty, value);
        }

        public static readonly BindableProperty AuthorProperty =
            BindableProperty.Create(nameof(Author), typeof(string), typeof(CommentView));

        public string Author
        {
            get => (string)GetValue(AuthorProperty);
            set => SetValue(AuthorProperty, value);
        }

        public CommentView()
        {
            InitializeComponent();
        }
    }
}