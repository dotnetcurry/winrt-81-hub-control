using DncShowcaseHub.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.AtomPub;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace DncShowcaseHub.Data
{


    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class FeedDataSource
    {
        private static FeedDataSource _sampleDataSource = new FeedDataSource();

        private ObservableCollection<FeedDataGroup> _groups = new ObservableCollection<FeedDataGroup>();
        public ObservableCollection<FeedDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<FeedDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetFeedDataAsync();
            return _sampleDataSource.Groups;
        }

        public static async Task<FeedDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetFeedDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<FeedDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetFeedDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        static public AtomPubClient GetClient()
        {
            AtomPubClient client;
            client = new AtomPubClient();
            client.BypassCacheOnRetrieve = true;
            client.ServerCredential = null;
            return client;
        }

        private async Task GetFeedDataAsync()
        {
            FeedItemIterator feedIterator = new FeedItemIterator();
            if (this._groups.Count != 0)
                return;
            Groups.Add(new FeedDataGroup("whatsnew", "What's New", "Latest articles on DNC", "http://feeds.feedburner.com/netCurryRecentArticles", "Latest DNC articles"));
            Groups.Add(new FeedDataGroup("aspnetmvc", "ASP.NET MVC", "Latest ASP.NET MVC articles","http://www.dotnetcurry.com/GetArticlesRss.aspx?CatID=67","Latest ASP.NET MVC articles"));
            Groups.Add(new FeedDataGroup("winrt", "Windows 8 and 8.1", "Articles for Windows 8 and 8.1 Store Apps", "http://www.dotnetcurry.com/GetArticlesRss.aspx?CatID=75", "Articles for Windows 8 and 8.1 Store Apps")); //WinRT
            Groups.Add(new FeedDataGroup("azure", "Windows Azure", "Windows Azure Tutorials", "http://www.dotnetcurry.com/GetArticlesRss.aspx?CatID=73", "Windows Azure Tutorials")); //Windows Azure
            Groups.Add(new FeedDataGroup("aspnet", "ASP.NET and WebAPI", "Web API articles", "http://www.dotnetcurry.com/GetArticlesRss.aspx?CatID=54", "ASP.NET Web API Articles")); //ASP.NET
            Groups.Add(new FeedDataGroup("vstfs", "Visual Studio and Team Foundation Server", "Visual Studio and TFS Articles", "http://www.dotnetcurry.com/GetArticlesRss.aspx?CatID=60",  "Visual Studio and Team Foundation Server"));  //Visual Studio Team System
            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");
            Uri resourceUri;
            foreach (var group in Groups)
            {
                if (!Uri.TryCreate(group.FeedPath, UriKind.Absolute, out resourceUri))
                {
                    return;
                }
                try
                {
                    feedIterator.AttachFeed(await FeedDataSource.GetClient().RetrieveFeedAsync(resourceUri));
                    while (feedIterator.MoveNext())
                    {
                        FeedDataItem item = new FeedDataItem(group.UniqueId,
                                                            feedIterator.GetTitle(),
                                                            feedIterator.GetAuthor(),
                                                            feedIterator.GetUrlPath(),
                                                            feedIterator.GetContent(),
                                                            feedIterator.GetContent());
                        group.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}