using VertiGIS.Mobile.Samples.SampleSelector;
using VertiGIS.Mobile.Infrastructure.App;
using VertiGIS.Mobile.Infrastructure.Configuration;
using VertiGIS.Mobile.Toolkit.Views;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using VertiGIS.Mobile.Toolkit.Views.Markdown;
using Xamarin.Forms;

namespace VertiGIS.Mobile.Samples
{
    public class App : Application
    {
        public static App SamplesInstance;
        public LoadAppResult LoadResult;

        public App() : base()
        {
            AppManager.Initialize(this);

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
            if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP)
            {
                AppManager.Instance.OnActivated();
            }

            base.OnResume();
        }

        public async Task LoadApp(Sample sample)
        {
            // Push a loading spinner.
            if (Device.RuntimePlatform != Device.iOS)
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
                LoadResult = await AppManager.Instance.Bootstrapper.LoadAppAysnc(app);
            }
            else
            {
                // Load the main VertiGIS Studio Mobile app page.
                LoadResult = await AppManager.Instance.Bootstrapper.LoadAppAysnc(app, layout);
            }

            LoadResult.Page.Title = "Demo";
            LoadResult.Page.IconImageSource = "demo.png";

            var tabbedPage = new TabbedPage()
            {
                Title = sample.Name,
                BarBackgroundColor = Color.LightGray,
                BarTextColor = Color.Black
            };

            var description = GetDescription(readme);

            // iOS apps with Navigation/TabbedPage/HorizontalTaskbar are being problematic.
            // Use PushModalAsync instead of PushAsync and inject a button to return to samples.
            if (Device.RuntimePlatform == Device.iOS && Device.Idiom == TargetIdiom.Phone)
            {
                var content = (LoadResult.Page as ContentPage).Content as RelativeLayout;
                var verticalPadding = DependencyService.Get<VertiGIS.Mobile.Infrastructure.Platform.ISafeArea>().GetInsets().Top;
                var appPage = new ContentPage() { Content = content, Title = "Demo", IconImageSource = "demo.png" };
                var descriptionPage = new ContentPage { Content = description, Title = "Description", IconImageSource = "description.png", Padding = new Thickness(0, verticalPadding, 0, 0) };

                tabbedPage.Children.Add(appPage);
                tabbedPage.Children.Add(descriptionPage);

                // Inject our `Return to Samples` button
                var button = new Button() { Text = "Return to Samples", BackgroundColor = Color.White, TextColor = Color.Black };
                button.Clicked += async (s, e) =>
                {
                    await MainPage.Navigation.PopModalAsync();
                };
                content.Children.Add(button, Constraint.Constant(10), Constraint.Constant(verticalPadding));

                await MainPage.Navigation.PushModalAsync(tabbedPage, false);

                return;
            }

            tabbedPage.Children.Add(LoadResult.Page);
            tabbedPage.Children.Add(new ContentPage { Content = description, Title = "Description", IconImageSource = "description.png" });

            await MainPage.Navigation.PushAsync(tabbedPage, false);

            // Pop the loading spinner.
            if (Device.RuntimePlatform != Device.iOS)
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
                LabelStyleOverrides = new Style(typeof(Label)) { Setters = { new Setter { Property = Label.TextColorProperty, Value = Color.Black } } }
            };

            var scrollContainer = new ScrollView() { Content = view.Content, HeightRequest = 1000, Margin = 5 };

            var stack = new StackLayout()
            {
                BackgroundColor = Color.White,
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
            var stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 15
            };

            // Spinner
            var spinner = new EnhancedActivityIndicator()
            {
                IsRunning = true
            };
            spinner.WidthRequest = 75;
            spinner.HeightRequest = 75;
            stack.Children.Add(spinner);

            // Label
            var label = new Label()
            {
                TextColor = Color.Black
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
