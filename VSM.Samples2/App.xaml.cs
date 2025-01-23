using System.Reflection;
using VertiGIS.Mobile;
using VertiGIS.Mobile.Infrastructure.App;
using VertiGIS.Mobile.Infrastructure.Configuration;

namespace VSM.Samples2
{
    public partial class App : Application
    {
        public static App SamplesInstance;
        public LoadAppResult LoadResult;

        public App()
        {
            AppManager.Initialize(this);

            InitializeComponent();



            MainPage = new ContentPage()
            {
                Content = GetContent()
            };

            // Register additional assemblies to search for configured assembly attributes.
            AppManager.Instance.AssemblyManager.RegisterAssemblies(this.GetType().Assembly);

            SamplesInstance = this;
        }

        protected override async void OnStart()
        {
            // Initialize VertiGIS Studio Mobile
            await AppManager.Instance.InitializeAsync();

            // Get our sample selection page and set it as the root.
            var selectorPage = new SampleSelector.Selector(this);

            MainPage = new NavigationPage(selectorPage)
            {
                Title = "VertiGIS Studio Mobile SDK Samples",
            };
        }


        /*
        OnSleep() and OnResume() don't seem to get called by Xamarin so we're providing the same functionality explicitly for iOS, Android.
        */

        // <inheritdoc/>
        protected override void OnSleep()
        {
            AppManager.Instance.OnBackgrounded();

            base.OnSleep();
        }

        // <inheritdoc/>
        protected override void OnResume()
        {
            // OnResume will get called when background processing begins.
            // UWP activated events are raised in UWP App.xaml.cs.
            if (DeviceInfo.Platform != DevicePlatform.WinUI)
            {
                AppManager.Instance.OnActivated();
            }

            base.OnResume();
        }

        public async Task LoadApp(Sample sample)
        {
            // Push a loading spinner.
            if (DeviceInfo.Platform != DevicePlatform.iOS)
            {
                await MainPage.Navigation.PushModalAsync(new ContentPage()
                {
                    Content = GetContent()
                });
            }

            // Configure some paths.
            var app = new Uri("resource://" + sample?.App);
            var layout = string.IsNullOrEmpty(sample?.Layout) ? null : new Uri("resource://" + sample?.Layout);
            var readme = $"VertiGIS.Mobile.Samples.Samples.{sample.PathFragment}.README.md";

            if (layout == null)
            {
                // If we don't have a layout, assume it's available in the app.
                LoadResult = await AppManager.Instance.Bootstrapper.LoadAppAsync(app);
            }
            else
            {
                // Load the main VertiGIS Studio Mobile app page.
                LoadResult = await AppManager.Instance.Bootstrapper.LoadAppAsync(app, layout);
            }

            LoadResult.Page.Title = "Demo";
            LoadResult.Page.IconImageSource = "demo.png";

            var tabbedPage = new TabbedPage()
            {
                Title = sample.Name,
                BarBackgroundColor = Colors.LightGray,
                BarTextColor = Colors.Black
            };

            var description = GetDescription(readme);

            tabbedPage.Children.Add(LoadResult.Page);
            tabbedPage.Children.Add(new ContentPage { Content = description, Title = "Description", IconImageSource = "description.png" });

            await MainPage.Navigation.PushAsync(tabbedPage, false);

            // Pop the loading spinner.
            if (DeviceInfo.Platform != DevicePlatform.iOS)
            {
                await MainPage.Navigation.PopModalAsync();
            }
        }

        public View GetDescription(string resource)
        {
            // Get our markdown content.
            string readmeContent;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    readmeContent = reader.ReadToEnd();
                }
            }

            // Create our markdown description.
            var view = new MarkdownView()
            {
                Markdown = readmeContent,
                LabelStyleOverrides = new Style(typeof(Label)) { Setters = { new Setter { Property = Label.TextColorProperty, Value = Colors.Black } } }
            };

            var scrollContainer = new ScrollView() { Content = view.Content, HeightRequest = 1000, Margin = 5 };

            var stack = new StackLayout()
            {
                BackgroundColor = Colors.White,
                Children =
                {
                    scrollContainer
                }
            };

            return stack;
        }

        public View GetContent()
        {
            // Stack
            var stack = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 15
            };

            // Spinner
            var spinner = new ActivityIndicator()
            {
                IsRunning = true
            };
            spinner.WidthRequest = 75;
            spinner.HeightRequest = 75;
            stack.Children.Add(spinner);

            // Label
            var label = new Label()
            {
                TextColor = Colors.Black
            };
            stack.Children.Add(label);

            void ShowStatus(object sender, LoadingEventArgs e)
            {
                if (GlobalConfiguration.Instance.StartupLoadStatus)
                {
                    label.Text = e.LoadAction;
                }
                else
                {
                    AppManager.LoadingAction -= ShowStatus;
                }
            }

            AppManager.LoadingAction += ShowStatus;

            return stack;
        }
    }
}
