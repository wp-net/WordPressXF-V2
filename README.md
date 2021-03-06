# WordPressXF - V2
This is a Xamarin.Forms app (supporting Android and iOS) designed to turn easily WordPress Blogs / Sites into nice little apps.

It's built on
* [WordPressPCL (WordPress REST API Wrapper)](https://github.com/wp-net/WordPressPCL)
* [Xamarin.Forms 5.0](https://github.com/xamarin/Xamarin.Forms)


## Features

working and planned features for WordPressXF:
- [x] Show posts
- [x] Show comments
- [x] Custom app icons
- [x] Use SplashScreen
- [x] Sign In
- [x] Add comment


## Screenshots

<img src="docs/wordpressxf-v2-01.png" alt="Screenshot-01" width="400"/>

<br/>

<img src="docs/wordpressxf-v2-02.png" alt="Screenshot-02" width="400"/>


# Quickstart

## WordPress Plugins

Since WordPress 4.7 the REST API has been integrated into the core so there's no need for any plugins to get basic functionality. If you want to access protected endpoints, this library supports authentication through JSON Web Tokens (JWT) (plugin required).

* [WordPress 4.7 or newer](https://wordpress.org/)
* [JWT Authentication for WP REST API](https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/)


## Getting Started

Just clone or download the repo and open it in Visual Studio. Before you can build you'll need to enter the URL to your WordPress blog/site in the file [WordPressXF/WordPressXF/WordPressXF/Common/Statics.cs](https://github.com/wp-net/WordPressXF-V2/blob/main/WordPressXF/WordPressXF/WordPressXF/Common/Statics.cs). And don't forget to add `/wp-json/` at the end.

```c#
namespace WordPressXF.Common
{
    public static class Statics
    {
        public static string WordpressUrl = "https://wordpress.org/news/wp-json/";

        // other properties
    }
}
```
