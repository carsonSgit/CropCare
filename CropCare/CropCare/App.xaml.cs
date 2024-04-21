using CropCare.DataRepos;
using CropCare.Models;
using MauiFitness.Config;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CropCare
{
    public partial class App : Application
    {

        public static Settings Settings { get; private set; }
        public static User CurrentUser { get; set; }
        private static CropCareRepo repo;
        public static CropCareRepo Repo
        {
            get
            {
                return repo ??= new CropCareRepo();
            }
        }
        public App()
        {
            InitializeComponent();
            var a = Assembly.GetExecutingAssembly();
            var stream = a.GetManifestResourceStream("CropCare.appsettings.json");

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
            Settings = config.GetRequiredSection(nameof(Settings)).Get<Settings>();
            MainPage = new AppShell();
        }
    }
}
