using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using CarouselView.FormsPlugin.Android;
using Acr.UserDialogs;

namespace App2.Droid
{
    [Activity(Label = "App2", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            CarouselViewRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}