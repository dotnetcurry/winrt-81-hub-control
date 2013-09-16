using DncShowcaseHub.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DncShowcaseHub.DataModel
{
    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class FeedDataGroup
    {
        public FeedDataGroup(String uniqueId, String title, String subtitle, String feedPath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.FeedPath = feedPath;
            this.Items = new ObservableCollection<FeedDataItem>();
        }
        
        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }

        public string FeedPath { get; set; }
        public ObservableCollection<FeedDataItem> Items { get; private set; }

        public FeedDataItem HeroItem
        {
            get
            {
                if (Items.Count > 0)
                {
                    return Items[0];
                }
                return null;
            }
        }
        public override string ToString()
        {
            return this.Title;
        }
    }
}
